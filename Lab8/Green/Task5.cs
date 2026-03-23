using System;
using System.Linq;

namespace Lab8.Green
{
    public class Task5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _markCounter;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => _marks?.ToArray();

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0 || _markCounter == 0) return 0;

                    double sum = 0;
                    for (int i = 0; i < _markCounter; i++)
                    {
                        sum += _marks[i];
                    }
                    sum /= _markCounter;
                    return sum;
                }
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _markCounter = 0;
            }

            public void Exam(int mark)
            {
                if (mark < 2 || mark > 5) return;
                if (_markCounter < 5)
                {
                    _marks[_markCounter++] = mark;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Surname: {_surname}");
                Console.WriteLine($"AverageMark: {AverageMark:F2}");
            }
        }

        public class Group
        {
            protected string _name;
            protected Student[] _students;
            protected int _studentCount;

            public string Name => _name ?? string.Empty;

            public Student[] Students
            {
                get
                {
                    if (_students == null) return new Student[0];
                    return _students.Take(_studentCount).ToArray();
                }
            }

            public virtual double AverageMark
            {
                get
                {
                    if (_studentCount == 0) return 0;
                    double sum = 0;
                    int validStudents = 0;

                    for (int i = 0; i < _studentCount; i++)
                    {
                        if (_students[i].AverageMark > 0)
                        {
                            sum += _students[i].AverageMark;
                            validStudents++;
                        }
                    }

                    if (validStudents == 0) return 0;
                    return sum / validStudents;
                }
            }

            public Group(string name)
            {
                _name = name ?? throw new ArgumentNullException(nameof(name));
                _students = new Student[10];
                _studentCount = 0;
            }

            public void Add(Student student)
            {
                if (_studentCount < _students.Length)
                {
                    _students[_studentCount++] = student;
                }
            }

            public void Add(Student[] students)
            {
                if (students == null) return;
                foreach (var student in students)
                {
                    Add(student);
                }
            }

            public static void SortByAverageMark(Group[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            Group temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"Group: {_name}");
                Console.WriteLine($"Average mark: {AverageMark:F2}");
                Console.WriteLine($"Students count: {_studentCount}");
            }
        }

        public class EliteGroup : Group
        {
            public EliteGroup(string name) : base(name)
            {
            }

            public override double AverageMark
            {
                get
                {
                    if (_studentCount == 0) return 0;

                    double totalWeightedSum = 0;
                    int totalStudentsWithMarks = 0;

                    for (int i = 0; i < _studentCount; i++)
                    {
                        Student student = _students[i];
                        int[] marks = student.Marks;
                        int markCount = 0;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] != 0)
                            {
                                markCount++;
                            }
                        }

                        if (markCount > 0)
                        {
                            double studentWeightedSum = 0;
                            int validMarks = 0;

                            for (int j = 0; j < marks.Length; j++)
                            {
                                if (marks[j] != 0)
                                {
                                    switch (marks[j])
                                    {
                                        case 5:
                                            studentWeightedSum += 5 * 1.0;
                                            break;
                                        case 4:
                                            studentWeightedSum += 4 * 1.5;
                                            break;
                                        case 3:
                                            studentWeightedSum += 3 * 2.0;
                                            break;
                                        case 2:
                                            studentWeightedSum += 2 * 2.5;
                                            break;
                                    }
                                    validMarks++;
                                }
                            }

                            if (validMarks > 0)
                            {
                                totalWeightedSum += studentWeightedSum / validMarks;
                                totalStudentsWithMarks++;
                            }
                        }
                    }

                    if (totalStudentsWithMarks == 0) return 0;
                    return totalWeightedSum / totalStudentsWithMarks;
                }
            }

            public override void Print()
            {
                Console.WriteLine($"Elite Group: {_name}");
                Console.WriteLine($"Average mark (with weights): {AverageMark:F2}");
                Console.WriteLine($"Students count: {_studentCount}");
            }
        }

        public class SpecialGroup : Group
        {
            public SpecialGroup(string name) : base(name)
            {
            }

            public override double AverageMark
            {
                get
                {
                    if (_studentCount == 0) return 0;

                    double totalWeightedSum = 0;
                    int totalStudentsWithMarks = 0;

                    for (int i = 0; i < _studentCount; i++)
                    {
                        Student student = _students[i];
                        int[] marks = student.Marks;
                        int markCount = 0;

                        for (int j = 0; j < marks.Length; j++)
                        {
                            if (marks[j] != 0)
                            {
                                markCount++;
                            }
                        }

                        if (markCount > 0)
                        {
                            double studentWeightedSum = 0;
                            int validMarks = 0;

                            for (int j = 0; j < marks.Length; j++)
                            {
                                if (marks[j] != 0)
                                {
                                    switch (marks[j])
                                    {
                                        case 5:
                                            studentWeightedSum += 5 * 1.0;
                                            break;
                                        case 4:
                                            studentWeightedSum += 4 * 0.75;
                                            break;
                                        case 3:
                                            studentWeightedSum += 3 * 0.5;
                                            break;
                                        case 2:
                                            studentWeightedSum += 2 * 0.25;
                                            break;
                                    }
                                    validMarks++;
                                }
                            }

                            if (validMarks > 0)
                            {
                                totalWeightedSum += studentWeightedSum / validMarks;
                                totalStudentsWithMarks++;
                            }
                        }
                    }

                    if (totalStudentsWithMarks == 0) return 0;
                    return totalWeightedSum / totalStudentsWithMarks;
                }
            }

            public override void Print()
            {
                Console.WriteLine($"Special Group: {_name}");
                Console.WriteLine($"Average mark (with weights): {AverageMark:F2}");
                Console.WriteLine($"Students count: {_studentCount}");
            }
        }
    }
}
