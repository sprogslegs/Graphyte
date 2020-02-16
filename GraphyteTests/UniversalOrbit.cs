using Graphyte;
using System;
using Xunit;
using FluentAssertions;

namespace GraphyteTests
{
    public class UniversalOrbit
    {
        [Fact]
        public void CountTotalChildBranches_Calculates_DirectAndIndirectRelationships()
        {
            var BodyB = new Node<string>("BAA");
            var BodyC = new Node<string>("CBB");
            var BodyD = new Node<string>("DCC");
            var BodyE = new Node<string>("EDD");
            var BodyF = new Node<string>("FEE");
            var BodyG = new Node<string>("GFF");
            var BodyH = new Node<string>("HGG");
            var BodyI = new Node<string>("IHH");
            var BodyJ = new Node<string>("JII");
            var BodyK = new Node<string>("KJJ");
            var BodyL = new Node<string>("LKK");
            var COM = new Node<string>("COM");

            var galaxy = new Tree<string>(COM);
                        
            BodyB.Neighbours.Add(BodyC);
            BodyB.Neighbours.Add(BodyG);
            BodyC.Neighbours.Add(BodyD);
            BodyD.Neighbours.Add(BodyE);
            BodyD.Neighbours.Add(BodyI);
            BodyE.Neighbours.Add(BodyF);
            BodyE.Neighbours.Add(BodyJ);
            BodyG.Neighbours.Add(BodyH);
            BodyJ.Neighbours.Add(BodyK);
            BodyK.Neighbours.Add(BodyL);
            COM.Neighbours.Add(BodyB);

            galaxy.AddNodes(BodyB, BodyC, BodyD, BodyE, BodyF, BodyG, BodyH, BodyI, BodyJ, BodyK, BodyL, COM);

            var result = CountTotalChildBranches(COM);

            result.Should().Be(42);
        }

        public int CountTotalChildBranches(Node<string> origin, int depth = 0)
        {
            var totalOrbit = origin.Neighbours.Count + depth;
            depth += 1;

            foreach (var neighbour in origin.Neighbours)
            {
                totalOrbit += CountTotalChildBranches(neighbour, depth) - 1;
            }

            return totalOrbit;
        }
    }
}
