using System;
using System.Threading;


namespace BinarySearchTree
{
    /* The Node class consists of three data members, the information, reference to the right child, and reference to the left child. */
    class Node
    {
        public int info { get; set; }
        public Node lchild { get; set; }
        public Node rchild { get; set; }
        public int level { get; set; }

        public Node(int i, Node l, Node r, int lv) /* Constructor for the Node class */
        {
            info = i;
            lchild = l;
            rchild = r;
            level = lv;
        }
    }
    class BinaryTree
    {
        Node ROOT;
        public BinaryTree()
        {
            ROOT = null;    /* Initializing ROOT to null */
        }

        public void insert(int element) /* Inserts a Node in the Binary Search Tree */
        {
            Node newnode, currentNode = ROOT, parent = null;
            int lvNode = 0;
            find(element, ref parent, ref currentNode, ref lvNode);
            if (currentNode != null) /* Checks if the node to tbe inserted is already present or not */
            {
                Console.WriteLine("Duplicates words not allowed");
                return;
            }
            else    /* If the specified Node is not present */
            {
                newnode = new Node(element, null, null, lvNode);    /* Creates a Node */
                if (parent == null)     /* If the tree is empty */
                    ROOT = newnode;
                else if (element < parent.info)
                    parent.lchild = newnode;
                else
                    parent.rchild = newnode;
            }
        }

        public void find(int element, ref Node parent, ref Node currentNode, ref int lv)
        {
            /* This function finds a given element in the tree and returns a variable containing the address of the corresponding node. It also returns a variable containing the address of the parent of the node. */
            while ((currentNode != null) && (currentNode.info != element))
            {
                parent = currentNode;
                if (element < currentNode.info)
                    currentNode = currentNode.lchild;
                else
                    currentNode = currentNode.rchild;
                lv++;
            }
        }

        public void inorder(Node ptr)   /* Performs the inorder traversal of the tree */
        {
            if (ROOT == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            if (ptr != null)
            {
                inorder(ptr.lchild);
                Console.WriteLine(ptr.info + ":" + ptr.level + "   ");
                inorder(ptr.rchild);
            }
        }

        public void preorder(Node ptr)  /* Performs the preorder traversal of the tree */
        {
            if (ROOT == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            if (ptr != null)
            {
                Console.WriteLine(ptr.info + ":" + ptr.level + "   ");
                preorder(ptr.lchild);
                preorder(ptr.rchild);
            }
        }

        public void postorder(Node ptr)     /* Performs the postorder traversal of the tree */
        {
            if (ROOT == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            if (ptr != null)
            {
                postorder(ptr.lchild);
                postorder(ptr.rchild);
                Console.WriteLine(ptr.info + ":" + ptr.level + "   ");
            }
        }

        public void remove(int element, Node Subtree)
        {
            Node currentNode = Subtree, parent = null;
            int lvNode = 0;
            find(element, ref parent, ref currentNode, ref lvNode);
            if (currentNode == null) // Khong tim duoc Delete Node
            {
                Console.WriteLine("Not Found");
                //return;
            }
            else if(currentNode.lchild == null && currentNode.rchild == null) // Xac dinh Delete Node la Leaf Node
            {
                if (currentNode == ROOT)
                {
                    ROOT = null;
                }
                else if (currentNode == parent.lchild)
                {
                    parent.lchild = null;
                }
                else
                {
                    parent.rchild = null;
                }
            }
            else if (currentNode.lchild == null ^ currentNode.rchild == null) // Xac dinh Delete Node co 1 child
            {
                Node Child = null;
                if (currentNode.lchild != null)
                {
                    Child = currentNode.lchild;
                    currentNode.lchild = null;
                }
                else
                {
                    Child = currentNode.rchild;
                    currentNode.rchild = null;
                }
                if (currentNode == ROOT)
                {
                    ROOT = Child;
                }
                else if(currentNode == parent.lchild)
                {
                    parent.lchild = Child;
                }
                else
                {
                    parent.rchild = Child;
                }
            }
            else////////////////////////////////////////////////////////////////// Xac dinh Delete Node co 2 child
            {
                Node Inorder_suc = currentNode.rchild;
                while (Inorder_suc.lchild != null)
                {
                    Inorder_suc = Inorder_suc.lchild;
                }
                if (currentNode == ROOT)
                {
                    int tmp = Inorder_suc.info;
                    Inorder_suc.info = ROOT.info;
                    ROOT.info = tmp;
                }
                else if (currentNode == parent.lchild)
                {
                    int tmp = Inorder_suc.info;
                    Inorder_suc.info = parent.lchild.info;
                    parent.lchild.info = tmp;
                }
                else
                {
                    int tmp = Inorder_suc.info;
                    Inorder_suc.info = parent.rchild.info;
                    parent.rchild.info = tmp;
                }
                remove(Inorder_suc.info,currentNode.rchild);
            }
            currentNode = null;
        }

        static void Main(string[] args)
        {
            BinaryTree b = new BinaryTree();
            b.insert(18);
            b.insert(10);
            b.insert(28);
            b.insert(9);
            b.insert(7);
            b.insert(19);
            b.insert(22);
            b.insert(13);
            b.insert(34);
            b.insert(30);
            b.insert(55);
            while (true)
            {
                Console.WriteLine("\nMenu");
                Console.WriteLine("1. Iplement insert operation");
                Console.WriteLine("2. Perform inorder traversal");
                Console.WriteLine("3. Perform preorder traversal");
                Console.WriteLine("4. Perform postorder traversal");
                Console.WriteLine("5. Iplement delete operation");
                Console.WriteLine("6. Exit");
                Console.WriteLine("\nEnter your choice (1-6): ");
                char ch = Convert.ToChar(Console.ReadLine());
                Console.WriteLine();
                switch (ch)
                {
                    case '1':
                        {
                            Console.Write("Enter a word: ");
                            int word = Convert.ToInt32(Console.ReadLine());
                            b.insert(word);
                        }
                        break;
                    case '2':
                        {
                            b.inorder(b.ROOT);
                        }
                        break;
                    case '3':
                        {
                            b.preorder(b.ROOT);
                        }
                        break;
                    case '4':
                        {
                            b.postorder(b.ROOT);
                        }
                        break;
                    case '5':
                        {
                            Console.Write("Enter a word: ");
                            int word = Convert.ToInt32(Console.ReadLine());
                            b.remove(word,b.ROOT);
                        }
                        break;
                    case '6':
                        return;
                    default:
                        {
                            Console.WriteLine("Invalid option");
                            break;
                        }
                }
            }
            //b.insert(18);
            //b.insert(10);
            //b.insert(28);
            //b.insert(9);
            //b.insert(7);
            //b.insert(19);
            //b.insert(22);
            //b.insert(13);
            //b.insert(34);
            //b.insert(30);
            //b.insert(55);

            //b.remove(28,b.ROOT);

            Console.ReadLine();
        }
    }
}