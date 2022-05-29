using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laba2.Controllers;
using Laba2.Models;

using Xunit;

namespace Laba2.Tests
{
    using static Laba2.Models.PilotShort;
    using static Laba2.Models.PilotWrite;
    public class UnitTest1
    {
        private static Laba2Context CreateContext()
        {
            var options = new DbContextOptionsBuilder<Laba2Context>().UseInMemoryDatabase("Laba2").Options;
            Laba2Context abc = new Laba2Context(options);
            return abc;
        }
        [Fact]
        public async Task CreatePilot()
        {
            //ARRANGE

            using var context = CreateContext();
            PilotsController pilotsController = new PilotsController(context);

            //ACT

            PilotWrite pilot = new PilotWrite()
            {
                Name = "Омельченко Софія Степанівна",
                BirthDate = new DateTime(1990, 1, 20),
                Experience = 4
            };
            var result = await pilotsController.PostPilot(pilot);


            //ASSERT

            Assert.True(result.Result is RedirectToActionResult);
        }
        [Fact]
        public async Task PilotValidation()
        {
            //ARRANGE

            using var context = CreateContext();
            PilotsController pilotsController = new PilotsController(context);

            //ACT

            PilotWrite pilot = new PilotWrite(); 
            pilot.Experience = -10;
            var result = await pilotsController.PostPilot(pilot);



            //ASSERT
            Assert.True(result.Result is BadRequestObjectResult);           
        }
    }
}