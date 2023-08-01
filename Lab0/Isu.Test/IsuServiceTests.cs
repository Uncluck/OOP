using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;
using Xunit.Abstractions;

namespace Isu.Test
{
    public class IsuServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IsuService isu;

        public IsuServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            isu = new IsuService();
        }

        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group1 = isu.AddGroup(new GroupName("M3211"));
            Student student = isu.AddStudent(group1, "Melnik Dmitry");
            Assert.Equal(group1.GetNameStudent("Melnik Dmitry"), student);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group newGroup = isu.AddGroup(new GroupName("M3211"));
            for (int i = 0; i < Constans.MaxGroupSize; i++)
            {
                isu.AddStudent(newGroup, $"Student{i}");
            }

            Assert.Throws<GroupFullException>(() => isu.AddStudent(newGroup, "Dmitry"));
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<CourseException>(() => isu.AddGroup(new GroupName("hghg1")));
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = isu.AddGroup(new GroupName("M3211"));
            Group newGroup = isu.AddGroup(new GroupName("M3212"));
            Student student = isu.AddStudent(oldGroup, "Melnik Dmitry");
            isu.ChangeStudentGroup(student, newGroup);
            Assert.Equal(student, newGroup.GetNameStudent("Melnik Dmitry"));
        }
    }
}
