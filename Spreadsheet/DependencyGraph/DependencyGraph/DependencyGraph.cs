// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

using System.Collections.Generic;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private Dictionary<string, HashSet<string>> Dependees;
        private Dictionary<string, HashSet<string>> Dependent;
        private int size;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// 
        /// </summary>

        public DependencyGraph()
        {
        Dependees = new Dictionary<string,HashSet<string>>();
        Dependent = new Dictionary<string, HashSet<string>>();
             size = 0;
        }



    /// <summary>
    /// The number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size
    {
        get { return size; }
    }


    /// <summary>
    /// The size of dependees(s).
    /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
    /// invoke it like this:
    /// dg["a"]
    /// It should return the size of dependees("a")
    /// </summary>
    public int this[string s]
    {
      get 
      { 
      return Dependees[s].Count; 
      }
    }


    /// <summary>
    /// Reports whether dependents(s) is non-empty.
    /// </summary>
    public bool HasDependents(string s)
    {
            if (Dependent[s].Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
    }


    /// <summary>
    /// Reports whether dependees(s) is non-empty.
    /// </summary>
    public bool HasDependees(string s)
    {
            if (Dependees[s].Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
    }


    /// <summary>
    /// Enumerates dependents(s).
    /// </summary>
    public IEnumerable<string> GetDependents(string s)
    {
            HashSet<string> DependentsSet = new HashSet<string>();
            if (Dependent.ContainsKey(s))
            {
                foreach (string elements in Dependent[s])
                {
                    DependentsSet.Add(elements);
                }
                return DependentsSet;
            }
            else
            {
                throw new KeyNotFoundException();
            }
    }

    /// <summary>
    /// Enumerates dependees(s).
    /// </summary>
    public IEnumerable<string> GetDependees(string s)
    {
            if (Dependees.ContainsKey(s))
            {
                HashSet<string> DependeesSet = new HashSet<string>();
                foreach (string elements in Dependees[s])
                {
                    DependeesSet.Add(elements);
                }
                return DependeesSet;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }


    /// <summary>
    /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
    /// 
    /// <para>This should be thought of as:</para>   
    /// 
    ///   t depends on s
    ///
    /// </summary>
    /// <param name="s"> s must be evaluated first. T depends on S</param>
    /// <param name="t"> t cannot be evaluated until s is</param>        /// 
    public void AddDependency(string s, string t)
    {
        if(Dependent.ContainsKey(s))
        {
            if(!Dependent[s].Contains(t))
            {
                    Dependent[s].Add(t);
            }
        }
        if(!Dependent.ContainsKey(s))
        {
                HashSet<string> DependentSet = new HashSet<string>();
                DependentSet.Add(t);
                Dependent.Add(s, DependentSet);
        }

        if (Dependees.ContainsKey(t))
        {
            if (!Dependees[t].Contains(s))
            {
                    Dependees[t].Add(s);
            }
        }
        if(!Dependees.ContainsKey(t))
        {
            HashSet<string> DependeesSet = new HashSet<string>();
            DependeesSet.Add(t);
            Dependees.Add(s, DependeesSet);
        }
        size++;
    }


    /// <summary>
    /// Removes the ordered pair (s,t), if it exists
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    public void RemoveDependency(string s, string t)
    {
            if (Dependent.ContainsKey(s))
            {
                if (Dependent[s].Contains(t))
                {
                    Dependent[s].Remove(t);
                }
            }
           
            if(Dependees.ContainsKey(t))
            {
                if (Dependees[t].Contains(s))
                {
                    Dependees[t].Remove(s);
                    
                }
            }
            
            size--;
    }


    /// <summary>
    /// Removes all existing ordered pairs of the form (s,r).  Then, for each
    /// t in newDependents, adds the ordered pair (s,t).
    /// </summary>
    public void ReplaceDependents(string s, IEnumerable<string> newDependents)
    {
            if (Dependent.ContainsKey(s))
            {
                foreach(string elements in Dependent[s])
                {
                    Dependent[s].Remove(elements);
                    Dependees[elements].Remove(s);
                }
                foreach(string elements in newDependents)
                {
                    Dependent[s].Add(elements);
                    Dependees[elements].Add(s);
                }

            }

    }


    /// <summary>
    /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
    /// t in newDependees, adds the ordered pair (t,s).
    /// </summary>
    public void ReplaceDependees(string s, IEnumerable<string> newDependees)
    {
            if (Dependees.ContainsKey(s))
            {
                foreach (string elements in Dependees[s])
                {
                    Dependees[s].Remove(elements);
                    Dependent[elements].Remove(s);
                }
                foreach (string elements in newDependees)
                {
                    Dependees[s].Add(elements);
                    Dependent[elements].Add(s);
                }
            }
    }

    }

}
