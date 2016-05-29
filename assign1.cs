//------ Assignment 1 : Unification Algorithm

//------ Stud.Name: Baabu Aravind Vellaian Selvarajan
//------ Stud.Numb: 200339484





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace assign1
{
  class Unify
  {
    
    static void Main(string[] args)
    {
      while (true)
      {
      Again:
      
        Console.WriteLine("\n  Assignment 1 : Unification Algorithm");
        
	Console.Write("\n Enter term 1 : ");    // 1st input
        string t1 = Console.ReadLine();

        Console.Write("\n Enter term 2 : ");    // 2nd input
        string t2 = Console.ReadLine();
     
        string final = unify_with_occur_check(t1, t2);
        Console.WriteLine("\n" + include_result(final));
        
	Console.WriteLine("\n Click 'y' then 'Enter' to enter terms again");
        int click = Console.Read();
	if (click == 'y')
	{
	  Console.ReadLine();
	  Console.Clear();
          goto Again;
	}
	else break;
      }
	return;
    }
    
    private static string include_result(string final)
    {
      return final.Replace(' ', '\n');
    }

    static string unify_with_occur_check(string t1, string t2)
    {
      int value1, value2, value_len1, value_len2, value_Index1, value_Index2, dotter, s_Index, up_Case, low_Case, punct;
      string[] inputTerm1 = new string[250];
      string[] inputTerm2 = new string[250];

      string s_string1;
      string s_string2;

      char[] inputString1 = new char[250];
      char[] inputString2 = new char[250];

      if ((Convert.ToBoolean(check_constant(t1)) && Convert.ToBoolean(check_constant(t2))) || (t1.Length == 0 && t2.Length == 0)) // checks both are constant or empty
      {
        if(t1.CompareTo(t2) == 0)
	{
	  return "yes";
	}
	else
	{
	  return "no";
	}
      }
      
      else if ((check_variable(t1) == 1) && (check_constant(t2) == 1))  //checks for 1st is variable and 2nd is constant
      {
	return t1 + "=" + t2 + ' ';
      }

      else if ((check_constant(t1) == 1) && (check_variable(t2) == 1)) // checks for 1st is constant and 2nd is variable
      {
	return t2 + "=" + t1 + ' ';
      }

      else if ((check_variable(t1) == 1) && (check_variable(t2) == 1)) //checks for both are variable
      {
	if(t1.CompareTo(t2) == 0)
	{
	  return "yes";
	}
	return t1 + "=" + t2 + ' ';
      }

      else if ((check_constant(t1) == 1)) // checks for 1st is constant and 2nd is function
      {
	return "no";
      }

      else if ((check_constant(t2) == 1))  // checks for 1st is variable and 2nd is function
      {
	return "no";
      }

      else if ((check_variable(t1) == 1))
      {
	if (check_occur(t1, t2) == 1)
	{
	  return "no";
	}
	if (check_consistency(t2) == 0)
	{
	  return "no";
	}
	return t1 + "=" + t2 + ' ';
      }

      else if(check_variable(t2) == 1)  // checks for 1st is function and 2nd is variable
      {
	if (check_occur(t2, t1) == 1)
	{
	  return "no";
	}
	if (check_consistency(t1) == 0)
	{
	  return "no";
	}
	value_len1 = t1.Length;
	value_Index1 = 0;
	while ((t1[value_Index1] != '(') && (value_Index1 < value_len1))
	{
	  if ((check_lower(t1[value_Index1]) == 0) || (t1[value_Index1] == ','))
	  {
	    return "no";
	  }
	  value_Index1 = value_Index1 + 1;
	}
	return t2 + "=" + t1 + ' ';
      }
      else                                    // checks for both are functions
      {
      value_len1 = t1.Length;
      value_len2 = t2.Length;
	  
      value_Index1 = 0;
      while ((t1[value_Index1] != '(') && (value_Index1 < value_len1))
	  {   //checks function or not if yes do process for next string
	    if ((check_lower(t1[value_Index1]) == 0) || (t1[value_Index1] == ','))
	    {
	      return "no";
	    }
	
	    inputString1[value_Index1] = t1[value_Index1];
	    value_Index1 = value_Index1 + 1;
	  }
	  value_len1 = value_Index1;

	  value_Index1 = 0;
	  while ((t2[value_Index1] != '(') && (value_Index1 < value_len2))
	  {
	    if (check_lower(t2[value_Index1]) == 0 || (t2[value_Index1] == ','))
	    {
	      return "no";
	    }
	    inputString2[value_Index1] = t2[value_Index1];
	    value_Index1 = value_Index1 + 1;
	  }
	  value_len2 = value_Index1;

	  if (value_len1 == value_len2)
	  {
	    for(value_Index1 = 0; value_Index1 < value_len1; value_Index1++)
	    {
	      if (inputString1[value_Index1] != inputString2[value_Index1])
	      {
	        return "no";
	      }
	    }
	  }
	  else
	  {
	    return "no";
	  }
	  
	  value_Index1 = value_len1 + 1;
	  
	  value_len1 = t1.Length;
	  value_len2 = t2.Length;

	  t1 = allocate(t1, value_Index1, (value_len1 - value_Index1));
	  t2 = allocate(t2, value_Index1, (value_len2 - value_Index1));

	  value_len1 = t1.Length;

	  value1 = 0;
	  dotter = 0;

	  for (value_Index1 = 0; (value_Index1 < value_len1 && dotter >= 0); value_Index1++)
	  {
	    value_Index2 = 0;
	    up_Case = 1;
	    low_Case = 1;
	    punct = 0;

	    s_Index = value_Index1;

	    while ((dotter > 0 || (t1[value_Index1] != ',')) && (dotter != -1))
	    {
	      if (check_variable_upper(t1[value_Index1]) == 1)
	      {
		if (up_Case == 0)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  low_Case = 0;
		  punct = 0;
		}
	      }
	      else if (check_lower(t1[value_Index1]) == 1)
	      {
		if (low_Case == 0)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  up_Case = 0;
		  punct = 0;
		}
	      }
	      else if (t1[value_Index1] == '(')
	      {
		value_Index2 = value_Index2 + 1;
		value_Index1 = value_Index1 + 1;
		dotter = dotter + 1;
		up_Case = 1;
		low_Case = 1;
		punct = 0;
	      }
	      else if ((t1[value_Index1] == ')') && (dotter == 0))
	      {
		dotter = dotter - 1;
	      }
	      else if ((t1[value_Index1] == ')') && (dotter > 0 ))
	      {
		value_Index2 = value_Index2 + 1;
		value_Index1 = value_Index1 + 1;
		dotter = dotter - 1;
		up_Case = 1;
		low_Case = 1;
		punct = 0;
	      }
	      else if (t1[value_Index1] == ',')
	      {
		if (punct == 1)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  up_Case = 1;
		  low_Case = 1;
		  punct = 1;
		}
	      }
	    }

	    if ((dotter == -1) && (value_Index1 != (value_len1 - 1)))
	    {
	      return "no";
	    }
	
	    else if ((dotter == -1) || (t1[value_Index1] == ','))
	    {
	      inputTerm1[value1] = allocate(t1, s_Index, value_Index2);
	      value1 = value1 + 1;
	    }
	  }

	  value_len2 = t2.Length;

	  value2 = 0;
	  dotter = 0;

	  for (value_Index1 = 0; (value_Index1 < value_len2 && dotter >= 0); value_Index1++)
	  {
	    value_Index2 = 0;
	    up_Case = 1;
	    low_Case = 1;
	    punct = 0;

	    s_Index = value_Index1;

	    while ((dotter > 0 || (t2[value_Index1] != ',')) && (dotter != -1))
	    {
	      if (check_variable_upper(t2[value_Index1]) == 1)
	      {
	        if (up_Case == 0)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  low_Case = 0;
		  punct = 0;
		}
	      }
	      else if (check_lower(t2[value_Index1]) == 1)
	      {
		if (low_Case == 0)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  up_Case = 0;
		  punct = 0;
		}
	      }
	      else if (t2[value_Index1] == '\'')
	      {
		value_Index2 = value_Index2 + 1;
		value_Index1 = value_Index1 + 1;
		dotter = dotter + 1;
		up_Case = 1;
		low_Case = 1;
		punct = 0;
	      }
	      else if (t2[value_Index1] == 0 || t2[value_Index1] == 1 || t2[value_Index1] == 2 || t2[value_Index1] == 3 || t2[value_Index1] == 4 || t2[value_Index1] == 5 || t2[value_Index1] == 6 || t2[value_Index1] == 7 || t2[value_Index1] == 8 || t2[value_Index1] == 9)
	      {
		return "no";
	      }
	      else if (t2[value_Index1] == '(')
	      {
		value_Index2 = value_Index2 + 1;
		value_Index1 = value_Index1 + 1;
		dotter = dotter + 1;
		up_Case = 1;
		low_Case = 1;
		punct = 0;
	      }
	      else if ((t2[value_Index1] == ')') && (dotter == 0))
	      {
		dotter = dotter - 1;
	      }
	      else if((t2[value_Index1] == ')') && (dotter > 0))
	      {
		value_Index2 = value_Index2 + 1;
		value_Index1 = value_Index1 + 1;
		dotter = dotter - 1;
		up_Case = 1;
		low_Case = 1;
		punct = 0;
	      }
	      else if (t2[value_Index1] == ',')
	      {
		if(punct == 1)
		{
		  return "no";
		}
		else
		{
		  value_Index2 = value_Index2 + 1;
		  value_Index1 = value_Index1 + 1;
		  up_Case = 1;
		  low_Case = 1;
		  punct = 1;
		}
	      }
	    }
 
	    if ((dotter == -1) && (value_Index1 != (value_len2 - 1)))
	    {
	      return "no";
	    }
	    else if ((dotter == -1) || (t2[value_Index1] == ','))
	    {
	      inputTerm2[value2] = allocate(t2, s_Index, value_Index2);
	      value2 = value2 + 1;
	    }
	  }
	  if (value1 != value2) //checks no. of values are same
	  {
	    return "no";
	  }

	  s_string1 = "";

	  for (value_Index1 = 0; value_Index1 < value1; value_Index1++)
	  {
	    s_string2 = unify_with_occur_check(inputTerm1[value_Index1].ToString(), inputTerm2[value_Index1].ToString());
	    if (s_string2 != "no")
	    {
	      if (s_string2 != "yes")
	      {
		for (value_Index2 = value_Index1 + 1; value_Index2 < value1; value_Index2++)
		{
		  inputTerm1[value_Index2] = variable_substitute(s_string2, inputTerm1[value_Index2].ToString());
		  inputTerm2[value_Index2] = variable_substitute(s_string2, inputTerm2[value_Index2].ToString());
		}
	      }
	      else 
	      {
	      s_string2 = "";
	      }
	      s_string1 = formation_both_strings(s_string1, s_string2);
	    }
	    else 
	    {
	    s_string1 = s_string2;
	    break;
	    }
	  }
	  return s_string1;
	}
      }
      static string variable_substitute (string s_string, string term)
      {
	string[] a_String1 = new string[300];
	string[] a_String2 = new string[300];
	string n1, n2;

	int len_s_string;
	int i1, s_len, i2, s_Index;

	i2 = 0;
	len_s_string = s_string.Length;

	for (i1 = 0; i1 < len_s_string; i1++)
	{
	  s_len = 0;
	  s_Index = i1;
	  while (s_string[i1] != '=')
	  {
	    s_len = s_len + 1;
	    i1 = i1 + 1;
	  }	
	  a_String1[i2] = allocate(s_string, s_Index, s_len);
	  i1 = i1 + 1;

	  s_len = 0;
	  s_Index = i1;
	  while (s_string[i1] != ' ')
	  {
	    s_len = s_len + 1;
	    i1 = i1 + 1;
	  }
	  a_String2[i2] = allocate(s_string, s_Index, s_len);
	  i2 = i2 + 1;
	}

	len_s_string = i2;
	for(i1 = 0; i1 < len_s_string; i1++)
	{
	  s_len = index_replace(a_String1[i1], term);

	  if (s_len != -1)
	  {
	    n1 = allocate(term, 0, s_len);
	    n2 = allocate(term, (a_String1[i1].Length + s_len), (term.Length - s_len - 1));
	    term = n1 + a_String2[i1] + n2;
	    i1 = i1 - 1;
	  }
	}
	return term;
      }

      static int index_replace(string t1, string t2) // function for replacing substitutes
      {
	int i1, i2, i3;
	int len_t1, len_t2;
	char[] t_String1 = new char[200];
	char[] t_String2 = new char[200];
	
	len_t1 = t1.Length;
	len_t2 = t2.Length;

	t1.CopyTo(0, t_String2, 0, len_t1);

	for (i1 = 0; i1 <= (len_t2 - len_t1); i1++)
	{
	  t2.CopyTo(i1, t_String1, 0, len_t1);
	  i3 = 0;
	  for (i2 = 0; i2 < len_t1; i2++)
	  {
	    if(t_String1[i2] == t_String2[i2])
	    {
	      i3 = i3 + 1;
	    }
	  }
	  if (i1 > 0 && i1 < (len_t2 - len_t1))
	  {
	    if ((check_upper(t2[i1 - 1]) != 1) && (check_upper(t2[i1 + len_t1]) != 1) && (i3 == len_t1))
	    {
	      return i1;
	    }
	  }
	  else if (i1 > 0)
	  {
	    if ((check_upper(t2[i1 - 1]) != 1) && (i3 == len_t1))
	    {
	      return i1;
	    }
	  }
	  else if (i1 < (len_t2 - len_t1))
	  {
	    if ((check_upper(t2[i1 + len_t1]) != 1) && (i3 == len_t1))
	    {
	      return i1;
	    }
	  }
	    else if (i3 == len_t1)
	    {
	      return i1;
	    }
	  }
	  return -1;
	}

	static string formation_both_strings(string s_string1, string s_string2)      
        {
          string t_String;

          if (s_string1.Length == 0)   
          {
            return s_string2;
          }
          else if (s_string2.Length == 0)
          {
            return s_string1;
          }
          else
          {
            s_string1 = replace_subs(s_string2, s_string1);              
            t_String = check_equality_substring1(s_string1);          
            s_string2 = check_equality_substring2(s_string1, s_string2);    

          }

          return t_String + s_string2;
        }

	static string replace_subs(string s_string, string term)               
        {
          string[] a_String1 = new string[150];
          string[] a_String2 = new string[150];
          string t_String1, t_String2;

          int len_s_string;
          int i1, len, i2, s_Index;

          i2 = 0;
          len_s_string = s_string.Length;

          for (i1 = 0; i1 < len_s_string; i1++)
          {
            len = 0;
            s_Index = i1;
            while (s_string[i1] != '=')
            {
              len = len + 1;
              i1 = i1 + 1;
            }
            a_String1[i2] = allocate(s_string, s_Index, len);
            len = 0;
            i1 = i1 + 1;
            s_Index = i1;
            while (s_string[i1] != ' ')
            {
              len = len + 1;
              i1 = i1 + 1;
            }
            a_String2[i2] = allocate(s_string, s_Index, len);

            i2 = i2 + 1;
          }

          len_s_string = i2;

          for (i1 = 0; i1 < len_s_string; i1++)
          {
            len = replace_subs_help(a_String1[i1], term);

            if (len != -1 && len != 0)
            {
              t_String1 = allocate(term, 0, len);
              t_String2 = allocate(term, (a_String1[i1].Length + len), (term.Length - len - a_String1[i1].Length));
              term = t_String1 + a_String2[i1] + t_String2;
              i1 = i1 - 1;
            }
          }

          return term;
        }

	static int replace_subs_help(string t1, string t2)   
        {
          int s_Index, i1, cntr;
          int len_t1, len_t2;
          char[] s1 = new char[150];
          char[] s2 = new char[150];

          len_t1 = t1.Length;
          len_t2 = t2.Length;

          t1.CopyTo(0, s2, 0, len_t1);

          for (s_Index = 0; s_Index <= (len_t2 - len_t1); s_Index++)
          {
            t2.CopyTo(s_Index, s1, 0, len_t1);

            cntr = 0;

            for (i1 = 0; i1 < len_t1; i1++)
            {
              if (s1[i1] == s2[i1])
              {
                cntr = cntr + 1;
              }
            }

            if (s_Index > 0 && s_Index < (len_t2 - len_t1))
            {
              if ((check_upper(t2[s_Index - 1]) != 1) && (check_upper(t2[s_Index + len_t1]) != 1) && (cntr == len_t1) && (t2[s_Index + len_t1] != '='))
              {
                return s_Index;
              }
            }
            else if (s_Index > 0)
            {
              if ((check_upper(t2[s_Index - 1]) != 1) && (cntr == len_t1))
              {
                return s_Index;
              }
            }
            else if (s_Index < (len_t2 - len_t1))
            {
              if ((check_upper(t2[s_Index + len_t1]) != 1) && (cntr == len_t1) && (t2[s_Index + len_t1] != '='))
              {
                return s_Index;
              }
            }
            else if (cntr == len_t1 && (t2[s_Index + len_t1] != '='))
            {
              return s_Index;
            }

          }
          return -1;
        }


	static string check_equality_substring1(string s_string)
        {
          int s_Index, i1, c_Status, s_len, len_s_string, s1_len, s2_len;
          string S1, S2;

          char[] str1 = new char[150];
          char[] str2 = new char[150];

          len_s_string = s_string.Length;

          for (s_Index = 0; s_Index < len_s_string; s_Index++)
          {
            i1 = 0;
            s_len = s_Index;
            while (s_string[s_Index] != '=')
            {
              str1[i1] = s_string[s_Index];
              i1 = i1 + 1;
              s_Index = s_Index + 1;
            }
            s1_len = i1;

            i1 = 0;
            s_Index = s_Index + 1;
            while (s_string[s_Index] != ' ')
            {
              str2[i1] = s_string[s_Index];
              i1 = i1 + 1;
              s_Index = s_Index + 1;
            }
            s2_len = i1;

            if (s1_len == s2_len)
            {
              c_Status = 0;
              for (i1 = 0; i1 < s1_len; i1++)
              {
                 if (str1[i1] != str2[i1])
                 {
                   c_Status = c_Status + 1;
                 }
               }

               if (c_Status == 0)
               {
                 S1 = allocate(s_string, 0, s_len);
                 S2 = allocate(s_string, (s_Index + 1), (s_string.Length - s_Index - 1));
                 s_string = S1 + S2;
                 s_Index = s_len - 1;
                 len_s_string = s_string.Length;
               }
             }

          }

          return s_string;

	}
 
	static string check_equality_substring2(string s_String1, string s_String2)     
        {

          string[] t_String1 = new string[200];
          string[] t_String2 = new string[200];
          string s1, s2, t_String;

          int len_s_String1, len_s_String2, len_t_String1;

          int i1, i2, s_len, s_Index, i3;

          i2 = 0;
          len_s_String1 = s_String1.Length;

          for (i1 = 0; i1 < len_s_String1; i1++)
          {
            s_len = 0;
            s_Index = i1;
            while (s_String1[i1] != '=')
            {
              s_len = s_len + 1;
              i1 = i1 + 1;
            }
            t_String1[i2] = allocate(s_String1, s_Index, s_len);

            s_len = 0;
            i1 = i1 + s_len;
            s_Index = i1;
            while (s_String1[i1] != ' ')
            {
              s_len = s_len + 1;
              i1 = i1 + 1;
            }
            t_String2[i2] = allocate(s_String1, s_Index, s_len);

            i2 = i2 + 1;
          }
          len_t_String1 = i2;

          len_s_String2 = s_String2.Length;

          for (i3 = 0; i3 < len_t_String1; i3++)
          {

             for (i1 = 0; i1 < len_s_String2; i1++)
             {
               s_len = 0;
               s_Index = i1;
               while (s_String2[i1] != '=')
               {
                 s_len = s_len + 1;
                 i1 = i1 + 1;
                }
                t_String = allocate(s_String2, s_Index, s_len);
                i1 = i1 + 1;
                while (s_String2[i1] != ' ')
                {
                  i1 = i1 + 1;
                }

                if (t_String.CompareTo(t_String1[i3]) == 0)
                {
                  s1 = allocate(s_String2, 0, s_Index);
                  s2 = allocate(s_String2, (i1 + 1), (s_String2.Length - i1 - 1));
                  s_String2 = s1 + s2;
                  i1 = s_Index - 1;
                  len_s_String2 = s_String2.Length;
                }

              }
          }

          return s_String2;
	}
      
	static int check_constant(string c_Term)   // function for checking constants 
        {
          int c_Bool = 1;
          int SIZE, i_Term;
          SIZE = c_Term.Length;
          for (i_Term = 0; i_Term < SIZE; i_Term++)
          {
            if (c_Term[0] == c_Term[i_Term])
            {
              if (check_upper(c_Term[i_Term]) == 1)
              {
                c_Bool = 0;
              }
            }
            else if (c_Term[i_Term] == '(')
            {
              c_Bool = 0;
            }
            else if (c_Term[i_Term] == ')')
            {
              c_Bool = 0;
            }
            else if (c_Term[i_Term] == ',')
            {
              c_Bool = 0;
            }
          }
          return c_Bool;
	}

	static int check_variable(string c_Term)  // function to check varible or not  
        {
          int len_Term, i_Var;

          len_Term = c_Term.Length;

          for (i_Var = 0; i_Var < len_Term; i_Var++)
          {
            if (check_variable_upper(c_Term[i_Var]) != 1)
            {
              return 0;
            }
            else if (c_Term[i_Var] == '(')
            {
              return 0;
            }
            else if (c_Term[i_Var] == ')')
            {
              return 0;
            }
            else if (c_Term[i_Var] == ',')
            {
              return 0;
            }
          }
          return 1;
        }

	static int check_consistency(string term)            
        {
          int t_Length, i, dotter, up_Case, low_Case, punct;

          t_Length = term.Length;

          i = 0;
          while ((term[i] != '(') && (i < t_Length))
          {
            if ((check_lower(term[i]) == 0) || (term[i] == ','))
            if (term[i] != '_')
            {
              return 0;
            }
            i = i + 1;
           }
           term = allocate(term, i, (t_Length - i));

           t_Length = term.Length;

           dotter = 0;


           up_Case = 1;
           low_Case = 1;
           punct = 0;


           for (i = 0; i < t_Length; i++)
           {

             if (check_upper(term[i]) == 1)
             {
               if (up_Case == 0)
               {
                 return 0;
               }
               else
               {
                 low_Case = 0;
                 punct = 0;
               }
             }
             else if (check_lower(term[i]) == 1)
             {
               if (low_Case == 0)
               {
                 return 0;
               }
               else
               {
                 up_Case = 0;
                 punct = 0;
               }
             }
             else if (term[i] == '(')
             {
               dotter = dotter + 1;
               up_Case = 1;
               low_Case = 1;
               punct = 0;
             }
             else if ((term[i] == ')') && punct == 1)
             {
               return 0;
             }
             else if (term[i] == ')')
             {
               dotter = dotter - 1;
               up_Case = 1;
               low_Case = 1;
               punct = 0;
             }
             else if (term[i] == ',')
             {
               if (punct == 1)
               {
                 return 0;
               }
               else
               {
                 up_Case = 1;
                 low_Case = 1;
                 punct = 1;
               }
             }
             if (dotter == -1)
             {
               return 0;
             }
           }
           if (dotter != 0)
           {
             return 0;
           }
           return 1;
	}

	static private string allocate(string t, int s_Index, int len)
        {
          return t.Substring(s_Index, len);
        }
        static int check_occur(string t1, string t2)      
        {
          int i1, i2, i3;
          int len_t1, len_t2;
          char[] STR1 = new char[150];
          char[] STR2 = new char[150];

          len_t1 = t1.Length;
          len_t2 = t2.Length;

          t1.CopyTo(0, STR2, 0, len_t1);

          for (i1 = 0; i1 <= (len_t2 - len_t1); i1++)
          {
            t2.CopyTo(i1, STR1, 0, len_t1);

            i3 = 0;

            for (i2 = 0; i2 < len_t1; i2++)
            {
              if (STR1[i2] == STR2[i2])
              {
                i3 = i3 + 1;
              }
            }

            if (i1 > 0 && i1 < (len_t2 - len_t1))
            {
              if ((check_upper(t2[i1 - 1]) != 1) && (check_upper(t2[i1 + len_t1]) != 1) && (i3 == len_t1))
              {
                return 1;
              }
            }
            else if (i1 > 0)
            {
              if ((check_upper(t2[i1 - 1]) != 1) && (i3 == len_t1))
              {
                return 1;
              }
            }
            else if (i1 < (len_t2 - len_t1))
            {
              if ((check_upper(t2[i1 + len_t1]) != 1) && (i3 == len_t1))
              {
                return 1;
              }
            }
            else if (i3 == len_t1)
            {
              return 1;
            }

          }

          return 0;
        }
        static private int check_lower(char c_Check)  // function for lower case   
        { 
          int low_Case = 0;
          for (char c = 'a'; c <= 'z'; c++)
          {
            if (c == c_Check)
            {
              low_Case = 1;
              break;
             }
           }
           return low_Case;
        }

        static private int check_upper(char c_Check)  //  function for upper case   
        {
          int up_Case = 0;
          for (char c = 'A'; c <= 'Z'; c++)
          {
            if (c == c_Check)
            {
              up_Case = 1;
              break;
            }
          }
          return up_Case;
	}

        static private int check_variable_upper(char p)         
        {
          int upper = 0;
          for (char a = 'A'; a <= 'Z'; a++)
          {
            if (a == p)
            {
              upper = 1;
              break;
                }
            }
            
            string string1 = Convert.ToString(p);
            int v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14;
            v1 = string1.IndexOf("1");
            v2 = string1.IndexOf("2");
            v3 = string1.IndexOf("3");
            v4 = string1.IndexOf("4");
            v5 = string1.IndexOf("5");
            v6 = string1.IndexOf("6");
            v7 = string1.IndexOf("7");
            v8 = string1.IndexOf("8");
            v9 = string1.IndexOf("9");
            v10 = string1.IndexOf("_");
            v11 = string1.IndexOf("'");
            v12 = string1.IndexOf("!");
            v13 = string1.IndexOf("-");
            v14 = string1.IndexOf(" ");
            if (v1 != -1 || v2 != -1 || v3 != -1 || v4 != -1 || v5 != -1 || v6 != -1 || v7 != -1 || v8 != -1 || v9 != -1 || v10 != -1 || v11 != -1 || v12 != -1 || v13 != -1 || v14 != -1)
            {
                upper = 1;
            }
            return upper;
        }
    }
}





















