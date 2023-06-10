using System;

namespace PixelGameLibrary.DataStructures
{
    public class Node<T>
    {
        public Node<T> parent;
        public List<Node<T>> children;
    }
    
    public class BinaryHeap<T>
    {
        private Node<T> root;
        
    }
}