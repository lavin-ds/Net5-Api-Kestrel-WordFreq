using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WordFreqApi.Controllers;
using WordFreqApi.DB;
using WordFreqApi.Models;
using Xunit;

namespace Api.Test.Controller
{
    public class Tests
    {
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
                        
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet.Object;
        }

        [Fact]
        public async void Test1()
        {
            //Arrange
            var contextMock = new Mock<IDbContext>(MockBehavior.Loose);

            var hfWord1 = new HighFrequencyWord
            {
                Word = "that",
                Frequency = 4,
                HighFrequencyWordId = 1,
                SubmissionId = 1
            };

            var hfWord2 = new HighFrequencyWord
            {
                Word = "this",
                Frequency = 2,
                HighFrequencyWordId = 2,
                SubmissionId = 1
            };

            var lstHFWords = new List<HighFrequencyWord>{hfWord1, hfWord2};

            var source = new Source
            {
                Date = DateTime.Now,
                SourceId = 1,
                SubmissionId = 2,
                Url = "www.testurl.com"
            };

            var submission1 = new Submission
            {
                Content = "TestContent",
                SubmissionId = 1,
                Submitter = "TestSubmitter",
                Comment = "TestComment",
                Source = source,
                ContentFreq = lstHFWords
            };

            var submission2 = new Submission
            {
                Content = "TestContent",
                SubmissionId = 2,
                Submitter = "TestSubmitter",
                Comment = "TestComment",
                Source = source,
                ContentFreq = lstHFWords                
            };

            var submissionsList = new List<Submission>{submission1, submission2};            
            var sourceList = new List<Source>{source};

            contextMock.SetupAllProperties();
            contextMock.Setup(x=>x.SubmissionItems).Returns(GetQueryableMockDbSet(submissionsList));
            contextMock.Setup(x=>x.SourceItems).Returns(GetQueryableMockDbSet(sourceList));
            contextMock.Setup(x=>x.HighFrequencyWordItems).Returns(GetQueryableMockDbSet(lstHFWords));
            var controller = new WordFreqController(contextMock.Object);
                
            // Act            
            var response = controller.GetAllSubmissions();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Submission>>>(response);
            contextMock.Verify();
            Assert.Equal(2, actionResult.Value.Count());
            Assert.Equal("TestSubmitter", actionResult.Value.FirstOrDefault().Submitter);
        }
    }
}