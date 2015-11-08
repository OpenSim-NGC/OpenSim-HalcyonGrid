using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenSim.Framework;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace OpenSim.Framework.Tests
{
    [TestFixture]
    public class RenderMaterialsTests
    {
        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void RenderMaterial_OSDFromToTest()
        {
            RenderMaterial mat = new RenderMaterial ();
            OSD map = mat.GetOSD ();
            RenderMaterial matFromOSD = RenderMaterial.FromOSD (map);
            Assert.That (mat, Is.EqualTo (matFromOSD));
            Assert.That (matFromOSD.NormalID, Is.EqualTo (UUID.Zero));
            Assert.That (matFromOSD.NormalOffsetX, Is.EqualTo (0.0f));
            Assert.That (matFromOSD.NormalOffsetY, Is.EqualTo(0.0f));
            Assert.That (matFromOSD.NormalRepeatX, Is.EqualTo(1.0f));
            Assert.That (matFromOSD.NormalRepeatY, Is.EqualTo(1.0f));
            Assert.That (matFromOSD.NormalRotation, Is.EqualTo(0.0f));
            Assert.That (matFromOSD.SpecularOffsetX, Is.EqualTo(0.0f));
            Assert.That (matFromOSD.SpecularOffsetY, Is.EqualTo(0.0f));
            Assert.That (matFromOSD.SpecularRepeatX, Is.EqualTo(1.0f));
            Assert.That (matFromOSD.SpecularRepeatY, Is.EqualTo(1.0f));
            Assert.That (matFromOSD.SpecularRotation, Is.EqualTo(0.0f));
            Assert.That (matFromOSD.SpecularLightColor, Is.EqualTo(RenderMaterial.DEFAULT_SPECULAR_LIGHT_COLOR));
            Assert.That (matFromOSD.SpecularLightExponent, Is.EqualTo(RenderMaterial.DEFAULT_SPECULAR_LIGHT_EXPONENT));
            Assert.That (matFromOSD.EnvironmentIntensity, Is.EqualTo(RenderMaterial.DEFAULT_ENV_INTENSITY));
            Assert.That (matFromOSD.DiffuseAlphaMode, Is.EqualTo((byte)RenderMaterial.eDiffuseAlphaMode.DIFFUSE_ALPHA_MODE_BLEND));
            Assert.That (matFromOSD.AlphaMaskCutoff, Is.EqualTo(0));
        }

        [Test]
        public void RenderMaterial_ToFromBinaryTest()
        {
            RenderMaterial mat = new RenderMaterial ();
            RenderMaterials mats = new RenderMaterials ();
            UUID key = mat.MaterialID;
            mats.AddMaterial(mat);

            byte[] bytes = mats.ToBytes ();
            RenderMaterials newmats = RenderMaterials.FromBytes(bytes, 0);
            RenderMaterial newmat = newmats.FindMaterial(key);
            Assert.That (mat, Is.EqualTo(newmat));
        }

        [Test]
        public void RenderMaterial_CopiedMaterialGeneratesTheSameMaterialID()
        {
            RenderMaterial mat = new RenderMaterial();
            RenderMaterial matCopy = (RenderMaterial) mat.Clone();

            Assert.That(mat, Is.EqualTo(matCopy));
            Assert.That(mat.MaterialID, Is.EqualTo(matCopy.MaterialID));
        }

        [Test]
        public void RenderMaterial_DefaultConstructedMaterialsGeneratesTheSameMaterialID()
        {
            RenderMaterial mat = new RenderMaterial();
            RenderMaterial mat2 = new RenderMaterial();

            Assert.That(mat, Is.EqualTo(mat2));
            Assert.That(mat.MaterialID, Is.EqualTo(mat2.MaterialID));
        }

        [Test]
        public void RenderMaterial_DifferentMaterialsGeneratesDifferentMaterialID()
        {
            RenderMaterial mat = new RenderMaterial();
            RenderMaterial mat2 = new RenderMaterial();
            mat2.NormalID = UUID.Random();

            Assert.AreNotEqual(mat, mat2);
            Assert.AreNotEqual(mat.MaterialID, mat2.MaterialID);
        }

    }
}

