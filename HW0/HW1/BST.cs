using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class Node
    {
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public int Val { get; set; }
    }
    public class BST
    {
        private Node? Root { get; set; }

        //insert number into bst, don't allow duplicate
        public void insert(int newVal)
        {
            //make new node
            Node newNode = new()
            {
                Val = newVal
            };

            //if tree is empty, set to root
            if (this.Root == null)
            {
                this.Root = newNode;
                return;
            }
                
            //make node pointers
            Node cur = Root;
            Node next = Root;

            //itterate a temp to end of a branch, have another temp follow 1 behind
            while (next != null)
            {
                cur = next;
                if (newVal < next.Val) 
                    next = next.Left;
                else if (newVal > next.Val) 
                    next = next.Right;

                //if not less or greater, then it is a duplicate.
                else
                {
                    return;
                }

            }


            if (newVal < cur.Val)
                cur.Left = newNode;
            else
                cur.Right = newNode;


            return;
        }
    


        //print tree in sorted order
        public void printTreeOrdered()
        {
            preOrderPrint(this.Root);
        }

        private void preOrderPrint(Node n)
        {
            if (n is null)
            {
                return;
            }
            //print left side
            preOrderPrint(n.Left);

            //print current
            Console.WriteLine(n.Val);

            //print right side
            preOrderPrint(n.Right);

            return;
        }

        //get number of items in the tree
        public int numItems()
        {

            return getNumItems(this.Root);
        }

        private int getNumItems(Node n)
        {
            if (n is null)
            {
                return 0;
            }
            return getNumItems(n.Left) + getNumItems(n.Right) + 1;
        }

        //get number of levels in tree
        public int numLevels()
        {

            return getNumLevels(this.Root);
        }

        private int getNumLevels(Node n)
        {
            if (n is null)
            {
                return 0;
            }
            return Math.Max(getNumLevels(n.Left), getNumLevels(n.Right)) + 1;
        }

        public static int minLevels(int nodes)
        {
            return (int)(Math.Ceiling(Math.Log2(nodes + 1)));
        }
    }
}
