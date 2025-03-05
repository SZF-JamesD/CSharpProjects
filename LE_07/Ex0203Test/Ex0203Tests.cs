using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ex0203.Services;
using Ex0203.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Ex0203.Tests
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod]
        public void TryAddPerson_ShouldIncreaseCount()
        {
            // Arrange
            var service = new PersonService();
            int initialCount = service.PersonCount;

            // Act
            bool added = service.TryAddPerson("John", "Doe", 30, "555-1234");

            // Assert
            Assert.IsTrue(added, "Expected person to be added.");
            Assert.AreEqual(initialCount + 1, service.PersonCount, "Person count did not increase by one.");
        }

        [TestMethod]
        public void GetPersonsByAge_ShouldReturnCorrectFilteredList()
        {
            // Arrange
            var service = new PersonService();
            service.TryAddPerson("Alice", "Smith", 25, "555-0001");
            service.TryAddPerson("Bob", "Jones", 35, "555-0002");
            service.TryAddPerson("Charlie", "Brown", 20, "555-0003");

            // Act
            List<Person> result = service.TryGetPersonsByAge(30);

            // Assert
            // Expecting Alice (25) and Charlie (20) but not Bob (35)
            Assert.AreEqual(2, result.Count, "Expected two persons to be returned.");
            Assert.IsTrue(result.Any(p => p.FirstName == "Alice"));
            Assert.IsTrue(result.Any(p => p.FirstName == "Charlie"));
            Assert.IsFalse(result.Any(p => p.FirstName == "Bob"));
        }

        [TestMethod]
        public void GetAllPersons_ShouldReturnAllPersons()
        {
            // Arrange
            var service = new PersonService();
            service.TryAddPerson("Alice", "Smith", 25, "555-0001");
            service.TryAddPerson("Bob", "Jones", 35, "555-0002");

            // Act
            List<Person> result = service.TryGetAllPersons();

            // Assert
            Assert.AreEqual(2, result.Count, "Expected all added persons to be returned.");
        }

        [TestMethod]
        public void TryAddPerson_ShouldNotExceedMaximumOfTenPersons()
        {
            // Arrange
            var service = new PersonService();
            bool added = false;

            // Act: Try adding 11 persons
            for (int i = 0; i < 11; i++)
            {
                added = service.TryAddPerson("Test", "User" + i, 20 + i, "555-000" + i);
            }

            // Assert: The 11th person should not be added and count should be 10
            Assert.IsFalse(added, "Expected adding the 11th person to fail.");
            Assert.AreEqual(10, service.PersonCount, "Person count should not exceed 10.");
        }
    }
}

