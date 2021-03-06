﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AvroSchemaGenerator.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace AvroSchemaGenerator.Tests
{
    [Collection("GetSchemaTest")]
    public class GetSchemaTest
    {
        private readonly ITestOutputHelper _output;

        public GetSchemaTest(ITestOutputHelper output)
        {
            this._output = output;
        }
        [Fact]
        public void TestSimpleFoo()
        {
            var simple = typeof(SimpleFoo).GetSchema();
            _output.WriteLine(simple);
            Assert.Contains("Age", simple);
        }

        [Fact]
        public void TestFoo()
        {
            var simple = typeof(Foo).GetSchema();
            _output.WriteLine(simple);
            Assert.Contains("Age", simple);
        }
        [Fact]
        public void TestFooCustom()
        {
            var simplecu = typeof(FooCustom).GetSchema();
            _output.WriteLine(simplecu);
            Assert.Contains("EntryYear", simplecu);
            Assert.Contains("Level", simplecu);
        }
        [Fact]
        public void TestRecursiveArray()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Family\",\"fields\":[{\"name\":\"Members\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Person\",\"fields\":[{\"name\":\"FirstName\",\"type\":\"string\"},{\"name\":\"LastName\",\"type\":\"string\"},{\"name\":\"Schools\",\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"School\",\"fields\":[{\"name\":\"State\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Year\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Schools\",\"type\":[\"null\",\"School\"],\"default\":null}]}],\"default\":null},{\"name\":\"Books\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Book\",\"fields\":[{\"name\":\"Author\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Title\",\"type\":[\"null\",\"string\"],\"default\":null}]}}},{\"name\":\"Children\",\"type\":\"Person\"}]}}}]}";
            var actual = typeof(Family).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
        [Fact]
        public void TestDictionaryRecursiveArray()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Dictionary\",\"fields\":[{\"name\":\"Fo\",\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"SimpleFoo\",\"fields\":[{\"name\":\"Age\",\"type\":\"int\"},{\"name\":\"Name\",\"type\":\"string\"},{\"name\":\"FactTime\",\"type\":\"long\"},{\"name\":\"Point\",\"type\":\"double\"},{\"name\":\"Precision\",\"type\":\"float\"},{\"name\":\"Attending\",\"type\":\"boolean\"},{\"name\":\"Id\",\"type\":[\"null\",\"bytes\"],\"default\":null}]}],\"default\":null},{\"name\":\"Courses\",\"type\":{\"type\":\"array\",\"items\":\"string\"}},{\"name\":\"Families\",\"type\":{\"type\":\"map\",\"values\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Family\",\"fields\":[{\"name\":\"Members\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Person\",\"fields\":[{\"name\":\"FirstName\",\"type\":\"string\"},{\"name\":\"LastName\",\"type\":\"string\"},{\"name\":\"Schools\",\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"School\",\"fields\":[{\"name\":\"State\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Year\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Schools\",\"type\":[\"null\",\"School\"],\"default\":null}]}],\"default\":null},{\"name\":\"Books\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"Book\",\"fields\":[{\"name\":\"Author\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Title\",\"type\":[\"null\",\"string\"],\"default\":null}]}}},{\"name\":\"Children\",\"type\":\"Person\"}]}}}]}}}]}";
            var actual = typeof(Dictionary).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
        [Fact]
        public void TestDictionaryRecursive()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"DictionaryRecursive\",\"fields\":[{\"name\":\"Fo\",\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"SimpleFoo\",\"fields\":[{\"name\":\"Age\",\"type\":\"int\"},{\"name\":\"Name\",\"type\":\"string\"},{\"name\":\"FactTime\",\"type\":\"long\"},{\"name\":\"Point\",\"type\":\"double\"},{\"name\":\"Precision\",\"type\":\"float\"},{\"name\":\"Attending\",\"type\":\"boolean\"},{\"name\":\"Id\",\"type\":[\"null\",\"bytes\"],\"default\":null}]}],\"default\":null},{\"name\":\"Courses\",\"type\":{\"type\":\"array\",\"items\":\"string\"}},{\"name\":\"Diction\",\"type\":\"DictionaryRecursive\"}]}";
            var actual = typeof(DictionaryRecursive).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
        [Fact]
        public void TestEnums()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"MediaStream\",\"fields\":[{\"name\":\"Id\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Title\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Type\",\"type\":{\"type\":\"enum\",\"name\":\"MediaType\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Video\",\"Audio\"]}},{\"name\":\"Container\",\"type\":{\"type\":\"enum\",\"name\":\"MediaContainer\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Flv\",\"Mp3\",\"Avi\",\"Mp4\"]}},{\"name\":\"Media\",\"type\":[\"null\",\"bytes\"],\"default\":null}]}";
            var actual = typeof(MediaStream).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
        [Fact]
        public void TestAliasesList()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"ClassWithAliasesWithList\",\"aliases\":[\"InterLives\",\"CountrySide\"],\"fields\":[{\"name\":\"City\",\"aliases\":[\"TownHall\",\"Province\"],\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"State\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Movie\",\"aliases\":[\"PopularMovie\"],\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"InnerAliases\",\"fields\":[{\"name\":\"Container\",\"aliases\":[\"Media\"],\"type\":{\"type\":\"enum\",\"name\":\"MediaContainer\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Flv\",\"Mp3\",\"Avi\",\"Mp4\"]}},{\"name\":\"Title\",\"type\":[\"null\",\"string\"],\"default\":null}]}],\"default\":null},{\"name\":\"Popular\",\"aliases\":[\"PopularMediaType\"],\"type\":{\"type\":\"enum\",\"name\":\"MediaType\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Video\",\"Audio\"]}},{\"name\":\"Movies\",\"aliases\":[\"MovieCollection\"],\"type\":[\"null\",{\"type\":\"array\",\"items\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"MovieAliase\",\"aliases\":[\"Movies_Aliase\"],\"fields\":[{\"name\":\"Dated\",\"aliases\":[\"DateCreated\"],\"type\":\"long\"},{\"name\":\"Year\",\"aliases\":[\"ReleaseYear\"],\"type\":\"int\"},{\"name\":\"Month\",\"aliases\":[\"ReleaseMonth\"],\"type\":{\"type\":\"enum\",\"name\":\"Month\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"January\",\"February\",\"March\",\"April\",\"June\",\"July\"]}}]}}]}]}";
            var actual = typeof(ClassWithAliasesWithList).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
        [Fact]
        public void TestAliasesDictionary()
        {
            var expectSchema = "{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"ClassWithAliasesWithDictionary\",\"aliases\":[\"InterLives\",\"CountrySide\"],\"fields\":[{\"name\":\"City\",\"aliases\":[\"TownHall\",\"Province\"],\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"State\",\"type\":[\"null\",\"string\"],\"default\":null},{\"name\":\"Movie\",\"aliases\":[\"PopularMovie\"],\"type\":[\"null\",{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"InnerAliases\",\"fields\":[{\"name\":\"Container\",\"aliases\":[\"Media\"],\"type\":{\"type\":\"enum\",\"name\":\"MediaContainer\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Flv\",\"Mp3\",\"Avi\",\"Mp4\"]}},{\"name\":\"Title\",\"type\":[\"null\",\"string\"],\"default\":null}]}],\"default\":null},{\"name\":\"Popular\",\"aliases\":[\"PopularMediaType\"],\"type\":{\"type\":\"enum\",\"name\":\"MediaType\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"Video\",\"Audio\"]}},{\"name\":\"YearlyMovies\",\"aliases\":[\"MoviesByYear\"],\"type\":[\"null\",{\"type\":\"map\",\"values\":{\"type\":\"record\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"name\":\"MovieAliase\",\"aliases\":[\"Movies_Aliase\"],\"fields\":[{\"name\":\"Dated\",\"aliases\":[\"DateCreated\"],\"type\":\"long\"},{\"name\":\"Year\",\"aliases\":[\"ReleaseYear\"],\"type\":\"int\"},{\"name\":\"Month\",\"aliases\":[\"ReleaseMonth\"],\"type\":{\"type\":\"enum\",\"name\":\"Month\",\"namespace\":\"AvroSchemaGenerator.Tests\",\"symbols\":[\"January\",\"February\",\"March\",\"April\",\"June\",\"July\"]}}]}}]}]}";
            var actual = typeof(ClassWithAliasesWithDictionary).GetSchema();
            _output.WriteLine(actual);

            Assert.Equal(expectSchema, actual);
        }
    }

    public class SimpleFoo
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public string Name { get; set; }
        public long FactTime { get; set; }
        public double Point { get; set; }
        public float Precision { get; set; }
        public bool Attending { get; set; }
        public byte[] Id { get; set; }
    }
    public class Foo
    {
        public SimpleFoo Fo { get; set; }
        [Required]
        public List<string> Courses { get; set; }
        [Required]
        public Dictionary<string, string> Normal { get; set; }

    }
    public class Dictionary
    {
        public SimpleFoo Fo { get; set; }
        [Required]
        public List<string> Courses { get; set; }
        [Required]
        public Dictionary<string, Family> Families { get; set; }

    }
    public class DictionaryRecursive
    {
        public SimpleFoo Fo { get; set; }
        [Required]
        public List<string> Courses { get; set; }
        [Required]
        public Dictionary<string, DictionaryRecursive> Diction { get; set; }

    }
    public class FooCustom
    {
        public SimpleFoo Fo { get; set; }
        public List<Course> Courses { get; set; }
        public Dictionary<string, Lecturers> Departments { get; set; }
    }

    public class Course
    {
        [DefaultValue("200")]
        [Required]
        public string Level { get; set; }

        [Required]
        public int Year { get; set; }

        [DefaultValue("Closed")]
        public string State { get; set; }

        public string Gender { get; set; }
    }

    public class School
    {
        public string State { get; set; }
        public string Year { get; set; }
        public List<School> Schools { get; set; }
    }
    public class Lecturers
    {
        public int EntryYear { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }
    public class Family
    {
        [Required]
        public List<Person> Members { get; set; }
    }

    public class Person
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public School Schools { get; set; }
        public List<Book> Books { get; set; }
        [Required]
        public List<Person> Children { get; set; }
    }

    [Aliases("InterLives", "CountrySide")]
    public sealed class ClassWithAliasesWithList
    {
        [Aliases("TownHall", "Province")]
        public string City { get; set; }
        public string State { get; set; }
        [Aliases("PopularMovie")]
        public InnerAliases Movie { get; set; }

        [Aliases("PopularMediaType")]
        public MediaType Popular { get; set; }

        [Aliases("MovieCollection")]
        public List<MovieAliase> Movies { get; set; }
    }

    [Aliases("InterLives", "CountrySide")]
    public sealed class ClassWithAliasesWithDictionary
    {
        [Aliases("TownHall", "Province")]
        public string City { get; set; }
        public string State { get; set; }
        [Aliases("PopularMovie")]
        public InnerAliases Movie { get; set; }

        [Aliases("PopularMediaType")]
        public MediaType Popular { get; set; }

        [Aliases("MoviesByYear")]
        public Dictionary<int, MovieAliase> YearlyMovies { get; set; }
    }

    public sealed class InnerAliases
    {
        [Aliases("Media")]
        public MediaContainer Container { get; set; }
        public string Title { get; set; }
    }

    [Aliases("Movies_Aliase")]
    public sealed class MovieAliase
    {
        [Aliases("DateCreated")]
        public long Dated { get; set; }
        [Aliases("ReleaseYear")]
        public int Year { get; set; }
        [Aliases("ReleaseMonth")]
        public Month Month { get; set; }
    }
    public class MediaStream
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public MediaType Type { get; set; }
        public MediaContainer Container { get; set; }
        public byte[] Media { get; set; }

    }

    public enum Month
    {
        January,
        February,
        March, 
        April,
        June, 
        July
    }

    public enum MediaType
    {
        Video,
        Audio
    }

    public enum MediaContainer
    {
        Flv,
        Mp3,
        Avi,
        Mp4
    }
}
