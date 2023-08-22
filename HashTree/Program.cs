using System;

public class MyMapNode<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
    public MyMapNode<K, V> Next { get; set; }

    public MyMapNode(K key, V value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
}

public class MyHashTable<K, V>
{
    private LinkedList<MyMapNode<K, V>>[] buckets;
    private int numBuckets;

    public MyHashTable(int size)
    {
        numBuckets = size;
        buckets = new LinkedList<MyMapNode<K, V>>[numBuckets];
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = key.GetHashCode();
        return Math.Abs(hashCode % numBuckets);
    }

    public V Get(K key)
    {
        int bucketIndex = GetBucketIndex(key);
        LinkedList<MyMapNode<K, V>> bucket = buckets[bucketIndex];
        if (bucket != null)
        {
            foreach (var node in bucket)
            {
                if (node.Key.Equals(key))
                {
                    return node.Value;
                }
            }
        }
        return default(V);
    }

    public void Add(K key, V value)
    {
        int bucketIndex = GetBucketIndex(key);
        if (buckets[bucketIndex] == null)
        {
            buckets[bucketIndex] = new LinkedList<MyMapNode<K, V>>();
        }

        foreach (var node in buckets[bucketIndex])
        {
            if (node.Key.Equals(key))
            {
                node.Value = value;
                return;
            }
        }

        MyMapNode<K, V> newNode = new MyMapNode<K, V>(key, value);
        buckets[bucketIndex].AddLast(newNode);
    }

    public void Display()
    {
        for (int i = 0; i < numBuckets; i++)
        {
            if (buckets[i] != null)
            {
                foreach (var node in buckets[i])
                {
                    Console.WriteLine($"Key: {node.Key}, Value: {node.Value}");
                }
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        string sentence = "To be or not to be";
        string[] words = sentence.Split(' ');

        MyHashTable<string, int> wordFrequencyTable = new MyHashTable<string, int>(10);

        foreach (var word in words)
        {
            int currentFrequency = wordFrequencyTable.Get(word);
            wordFrequencyTable.Add(word, currentFrequency + 1);
        }

        Console.WriteLine("Word Frequency in the sentence:");
        wordFrequencyTable.Display();
    }
}