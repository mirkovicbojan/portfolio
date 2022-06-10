using Moq;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;
using TimeSheet.CustomExceptions;

namespace UnitTests
{
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> repo = new Mock<IMemberRepository>();

        [Fact]
        public void GetAll_ReturnsAllMembers()
        {
            //Arrange
            var members = new List<Member>();

            Member memberOne = new Member
            {
                memberID = 1,
                memberName = "Member One",
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            Member memberTwo = new Member
            {
                memberID = 2,
                memberName = "Member Two",
                hoursPerWeek = 40,
                username = "membertwo",
                email = "membertwo@gmail.com",
                status = 1,
                role = 2
            };
            members.Add(memberOne);
            members.Add(memberTwo);
            repo.Setup(m => m.GetAll()).Returns(members);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            IEnumerable<Member> memberList = memberService.GetAll();
            //Assert
            Assert.NotEmpty(memberList);
        }

        [Fact]
        public void GetOne_ExistingIdPassed_ReturnsCorrectMember()
        {
            //Arrange
            var expectedMember = new Member
            {
                memberID = 1,
                memberName = "Member One",
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.GetById(1)).Returns(expectedMember);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            var member = memberService.GetOne(1);
            //Assert
            Assert.Equal(expectedMember.memberName, member.memberName);
        }

        [Fact]
        public void GetOne_UnknownIdPassed_ReturnsKeyNotFoundException()
        {
            //Arrange
            var expectedMember = new Member
            {
                memberID = 1,
                memberName = "Member One",
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.GetById(1)).Returns(expectedMember);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            Action act = () => memberService.GetOne(2);
            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.Equal("Member with id: 2 wasn't found.", exception.Message);
        }

        [Fact]
        public void Save_NewMember_ReturnsMemberObject()
        {
            //Arrange
            var newMember = new Member
            {
                memberID = 1,
                memberName = "Member One",
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.Save(newMember)).Returns(newMember);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            var member = memberService.Save(newMember);
            //Assert
            repo.Verify(m => m.Save(newMember), Times.Once);
            Assert.Equal(newMember.memberID, member.memberID);
        }

        [Fact]
        public void Save_InvalidObject_ReturnsInvalidObjectParamsException()
        {
            //Arrange
            var newMember = new Member
            {
                memberID = 1,
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.Save(newMember)).Returns(newMember);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            Action act = () => memberService.Save(newMember);
            //Assert
            repo.Verify(m => m.Save(newMember), Times.Never);
            InvalidObjectParamsException exception = Assert.Throws<InvalidObjectParamsException>(act);
            Assert.Equal("Member name cannot be empty.", exception.Message);
        }

        [Fact]
        public void UpdateOne_ObjectExists_ReturnsObject()
        {
            //Arrange
            var updatedMember = new Member
            {
                memberID = 1,
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.Edit(updatedMember)).Returns(updatedMember);
            repo.Setup(m => m.GetById(1)).Returns(updatedMember);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            var updated = memberService.UpdateOne(updatedMember);
            //Assert
            repo.Verify(m => m.Edit(updatedMember), Times.Once);
            Assert.Equal(updatedMember.memberID, updated.memberID);
        }

        [Fact]
        public void DeleteOne_ExistingObject_CallsRepository()
        {
            //Arrange
            var forDeletion = new Member
            {
                memberID = 1,
                hoursPerWeek = 40,
                username = "memberone",
                email = "memberone@gmail.com",
                status = 1,
                role = 1
            };
            repo.Setup(m => m.Delete(forDeletion));
            repo.Setup(m => m.GetById(1)).Returns(forDeletion);
            MemberService memberService = new MemberService(repo.Object);
            //Act
            memberService.DeleteOne(forDeletion);
            //Assert
            repo.Verify(f => f.Delete(forDeletion), Times.Once);
        }
    }
}