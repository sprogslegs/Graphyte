﻿using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Graphyte;

namespace GraphyteTests
{
    public class BinarySearchTreeTest
    {
        private BinaryTreeNode<int> _root;
        private BinaryTreeNode<int> _node4;
        private BinaryTreeNode<int> _node5;
        private BinaryTreeNode<int> _node8;
        private BinaryTreeNode<int> _node15;
        private BinaryTreeNode<int> _node20;
        private BinaryTreeNode<int> _sutRoot;
        private BinaryTreeNode<int> _sutNode4;
        private BinaryTreeNode<int> _sutNode15;
        private BinaryTreeNode<int> _sutNode8;
        private BinaryTreeNode<int> _sutNode5;
        private BinaryTreeNode<int> _sutNode20;
        private BinarySearchTree<int> _tree;

        public BinarySearchTreeTest()
        {
            ConstructBaseTestTree();
        }

        [Fact]
        public void BinarySearchTreeConstructsAsInheritedTypes()
        {
            _tree.Root.Should().BeEquivalentTo(_sutRoot);
            _tree.Nodes.Count.Should().Be(6);
            _tree.Nodes.ForEach(n => n.Should().BeOfType<BinaryTreeNode<int>>());
        }

        [Fact]
        public void InsertByValue_AddsNodeToCorrectPosition()
        {
            var expectedNodes = new List<Node<int>>
            {
                _root, _node4, _node5, _node8, _node15, _node20,
            };

            var tree = new BinarySearchTree<int>(new BinaryTreeNode<int>(7));

            tree.InsertByValue(4);
            tree.InsertByValue(15);
            tree.InsertByValue(8);
            tree.InsertByValue(5);
            tree.InsertByValue(20);

            tree.Root.Should().BeEquivalentTo(_root);
            tree.Nodes.Should().HaveCount(expectedNodes.Count);
            tree.Nodes.Should().BeEquivalentTo(expectedNodes);
        }

        [Fact]
        public void InsertByValue_Throws_WhenDuplicateValue()
        {
            var tree = new BinarySearchTree<int>(_root);

            tree.Invoking(t => t.InsertByValue(7))
                .Should().Throw<Exception>()
                .WithMessage("There is already a node with this value in the tree. Go climb some other tree");
        }

        [Fact]
        public void Case1_DeleteByValue_ReplacesNodeWithLeftChild_IfHasNoRightChild()
        {
            var node18 = new BinaryTreeNode<int>(18);
            _node15.RightChild = node18;

            var expectedNodes = new List<Node<int>>
            {
                _root, _node4, _node5, _node8, _node15, node18,
            };

            _tree.Nodes.Add(node18);
            _sutNode20.LeftChild = node18;

            _tree.DeleteByValue(20);

            _tree.Nodes.Should().BeEquivalentTo(expectedNodes);
        }

        [Fact]
        public void Case2_DeleteByValue_ReplacesNodeWithRightChild_IfRightChildHasNoLeftChild()
        {
            _root.RightChild = _node20;
            _node20.LeftChild = _node8;

            var expectedNodes = new List<Node<int>>
            {
                _root, _node4, _node5, _node8, _node20,
            };

            _tree.DeleteByValue(15);

            _tree.Nodes.Should().BeEquivalentTo(expectedNodes);
        }
        
        [Fact]
        public void Case3_DeleteByValue_ReplacesNodeWithRightChildsLeftmostDescendant_IfRightChildHasLeftChild()
        {
            // arrange
            var node18 = new BinaryTreeNode<int>(18);
            var node16 = new BinaryTreeNode<int>(16);
            _root.RightChild = node16;
            node16.LeftChild = _node8;
            node16.RightChild = _node20;
            _node20.LeftChild = node18;

            var expectedNodes = new List<Node<int>>
            {
                _root, _node4, _node5, _node8, _node20, node18, node16
            };

            var sutNode18 = new BinaryTreeNode<int>(18);
            var sutNode16 = new BinaryTreeNode<int>(16);
            _sutNode20.LeftChild = sutNode18;
            sutNode18.LeftChild = sutNode16;
            _tree.Nodes.Add(sutNode18);
            _tree.Nodes.Add(sutNode16);

            // act
            _tree.DeleteByValue(15);

            // assert
            _tree.Nodes.Should().BeEquivalentTo(expectedNodes);
        }

        // TODO implement a find by value that uses Linq to traverse the _nodes list and benchmark vs this one
        [Fact]
        public void FindByValueReturnsCorrectNode()
        {
            var result = _tree.FindByValueRecursive(15);

            result.Should().Be(_node15);
        }

        [Fact]
        public void FindSmallestReturnsNodeWithSmallestValue()
        {
            var result = _tree.FindSmallest();

            result.Should().Be(_sutNode4);
        }

        [Fact]
        public void FindLargestReturnsNodeWithLargestValue()
        {
            var result = _tree.FindLargest();

            result.Should().Be(_sutNode20);
        }

        private void ConstructBaseTestTree()
        {
            _root = new BinaryTreeNode<int>(7);
            _node4 = new BinaryTreeNode<int>(4);
            _node5 = new BinaryTreeNode<int>(5);
            _node8 = new BinaryTreeNode<int>(8);
            _node15 = new BinaryTreeNode<int>(15);
            _node20 = new BinaryTreeNode<int>(20);

            _root.LeftChild = _node4;
            _root.RightChild = _node15;
            _node15.LeftChild = _node8;
            _node15.RightChild = _node20;
            _node4.RightChild = _node5;

            _sutRoot = new BinaryTreeNode<int>(7);
            _sutNode4 = new BinaryTreeNode<int>(4);
            _sutNode15 = new BinaryTreeNode<int>(15);
            _sutNode8 = new BinaryTreeNode<int>(8);
            _sutNode5 = new BinaryTreeNode<int>(5);
            _sutNode20 = new BinaryTreeNode<int>(20);

            _tree = new BinarySearchTree<int>(_sutRoot);
            _tree.AddNodes(_sutNode4, _sutNode5, _sutNode8, _sutNode15, _sutNode20);

            _sutRoot.LeftChild = _sutNode4;
            _sutRoot.RightChild = _sutNode15;
            _sutNode15.LeftChild = _sutNode8;
            _sutNode15.RightChild= _sutNode20;
            _sutNode4.RightChild = _sutNode5;
        }
    }
}
