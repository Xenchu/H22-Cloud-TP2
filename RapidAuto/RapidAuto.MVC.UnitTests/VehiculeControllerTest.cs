using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RapidAuto.MVC.Controllers;
using RapidAuto.MVC.Interfaces;
using RapidAuto.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RapidAuto.MVC.UnitTests
{
    public class VehiculeControllerTest
    {
        private readonly IVehiculeService _vehiculeService;
        private readonly IFichierService _fichierService;
        private readonly IFavorisService _favorisService;

        //[Fact]
        //public async void Create_ModelState_Est_Valide_Retourne_RedirectToAction()
        //{
        //    // Étant donné
        //    IFormFile image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");

        //    var fixture = new Fixture();
        //    var vehicule = fixture.Build<Vehicule>()
        //                          .With(v => v.Image1, image)
        //                          .With(v => v.Image2, image)
        //                          .Create<Vehicule>();

        //    var images = new List<IFormFile>() { vehicule.Image1, vehicule.Image2 };
        //    var strings = new List<string> { "Valeur binaire 1", "Valeur binaire 2" };

        //    // Mock IConfiguration
        //    var typeVehicule = new Mock<IConfigurationSection>();
        //    typeVehicule.Setup(s => s.Value)
        //                .Returns("Essence");
        //    var vehiculeSection = new Mock<IConfigurationSection>();
        //    vehiculeSection.Setup(s => s.GetChildren())
        //                   .Returns(new List<IConfigurationSection> { typeVehicule.Object});
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockConfiguration.Setup(c => c.GetSection("Vehicule:TypeDeVehicule"))
        //                     .Returns(vehiculeSection.Object);

        //    // Mock IVehiculeService
        //    var mockVehiculeProxy = new Mock<IVehiculeService>();
        //    mockVehiculeProxy.Setup(v => v.ObtenirVehicules())
        //                     .Returns(It.IsAny<Task<IEnumerable<Vehicule>>>());
        //    mockVehiculeProxy.Setup(v => v.ObtenirCodeUnique(vehicule.Modele))
        //                     .Returns(() => Task.FromResult<string>(vehicule.CodeUnique));

        //    // Mock IFichierService
        //    var mockFichierProxy = new Mock<IFichierService>();
        //    mockFichierProxy.Setup(f => f.ConvertirImagesEnBytes(images))
        //                    .Returns(() => Task.FromResult<List<string>>(strings));
        //    mockFichierProxy.Setup(f => f.AjouterImages(It.IsAny<List<string>>(), vehicule.CodeUnique))
        //                    .Returns(() => Task.FromResult<List<string>>(strings));

        //    // Mock IFavorisService
        //    var mockFavorisProxy = new Mock<IFavorisService>();

        //    // Mock ILogger<VehiculeController>
        //    var mockILogger = new Mock<ILogger<VehiculeController>>();

        //    var vehiculeController = new VehiculeController(mockConfiguration.Object, mockVehiculeProxy.Object, mockFichierProxy.Object, mockFavorisProxy.Object, mockILogger.Object);

        //    // Quand

        //    var actionResult = await vehiculeController.Create(vehicule);

        //    // Alors
        //    var redirectToActionResult = actionResult as RedirectToActionResult;
        //    redirectToActionResult.Should().NotBeNull();
        //    redirectToActionResult.ActionName.Should().Be("Index");
        //}

        //[Fact]
        //public async void Create_Vehicule_Image1_FileName_Est_Invalide_Retourne_ViewAvecModel_Et_ModelStateError()
        //{
        //    // Étant donné
        //    IFormFile image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.invalide");
        //    IFormFile image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");

        //    var fixture = new Fixture();
        //    var vehicule = fixture.Build<Vehicule>()
        //                          .With(v => v.Image1, image1)
        //                          .With(v => v.Image2, image2)
        //                          .Create<Vehicule>();

        //    // Mock IConfiguration
        //    var typeVehicule = new Mock<IConfigurationSection>();
        //    typeVehicule.Setup(s => s.Value)
        //                .Returns("Essence");
        //    var vehiculeSection = new Mock<IConfigurationSection>();
        //    vehiculeSection.Setup(s => s.GetChildren())
        //                   .Returns(new List<IConfigurationSection> { typeVehicule.Object });
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockConfiguration.Setup(c => c.GetSection("Vehicule:TypeDeVehicule"))
        //                     .Returns(vehiculeSection.Object);

        //    // Mock ILogger<VehiculeController>
        //    var mockILogger = new Mock<ILogger<VehiculeController>>();

        //    var vehiculeController = new VehiculeController(mockConfiguration.Object, _vehiculeService, _fichierService, _favorisService, mockILogger.Object);

        //    // Quand
        //    var viewResult = await vehiculeController.Create(vehicule) as ViewResult;

        //    // Alors
        //    viewResult.Should().NotBeNull();
        //    viewResult.ViewData["TypesDeVehicule"].Should().NotBeNull();
        //    viewResult.ViewData.ModelState["Image1"].Errors.FirstOrDefault().ErrorMessage.Should().Be("Attention, seulement les extensions jpg, jpeg et png sont supportées !");

        //    var viewResultModel = viewResult.Model as Vehicule;
        //    viewResultModel.Should().Be(vehicule);

        //}

        //[Fact]
        //public async void Create_Vehicule_Image2_FileName_Est_Invalide_Retourne_ViewAvecModel_Et_ModelStateError()
        //{
        //    // Étant donné
        //    IFormFile image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");
        //    IFormFile image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.invalide");

        //    var fixture = new Fixture();
        //    var vehicule = fixture.Build<Vehicule>()
        //                          .With(v => v.Image1, image1)
        //                          .With(v => v.Image2, image2)
        //                          .Create<Vehicule>();

        //    // Mock IConfiguration
        //    var typeVehicule = new Mock<IConfigurationSection>();
        //    typeVehicule.Setup(s => s.Value)
        //                .Returns("Essence");
        //    var vehiculeSection = new Mock<IConfigurationSection>();
        //    vehiculeSection.Setup(s => s.GetChildren())
        //                   .Returns(new List<IConfigurationSection> { typeVehicule.Object });
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockConfiguration.Setup(c => c.GetSection("Vehicule:TypeDeVehicule"))
        //                     .Returns(vehiculeSection.Object);

        //    // Mock ILogger<VehiculeController>
        //    var mockILogger = new Mock<ILogger<VehiculeController>>();

        //    var vehiculeController = new VehiculeController(mockConfiguration.Object, _vehiculeService, _fichierService, _favorisService, mockILogger.Object);

        //    // Quand
        //    var viewResult = await vehiculeController.Create(vehicule) as ViewResult;

        //    // Alors
        //    viewResult.Should().NotBeNull();
        //    viewResult.ViewData["TypesDeVehicule"].Should().NotBeNull();
        //    viewResult.ViewData.ModelState["Image2"].Errors.FirstOrDefault().ErrorMessage.Should().Be("Attention, seulement les extensions jpg, jpeg et png sont supportées !");

        //    var viewResultModel = viewResult.Model as Vehicule;
        //    viewResultModel.Should().Be(vehicule);
        //}
    }
}