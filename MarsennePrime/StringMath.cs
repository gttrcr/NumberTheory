using System;

namespace MarsennePrime
{
    class StringMath
    {
        private string _string;

        private static bool IsSmaller(string str1, string str2)
        {
            int n1 = str1.Length, n2 = str2.Length;

            if (n1 < n2)
                return true;
            if (n2 > n1)
                return false;

            for (int i = 0; i < n1; i++)
            {
                if (str1[i] < str2[i])
                    return true;
                else if (str1[i] > str2[i])
                    return false;
            }
            return false;
        }

        public StringMath(string str = "0")
        {
            _string = str;
        }

        public new string ToString()
        {
            return _string;
        }

        public static StringMath operator +(StringMath str1, StringMath str2)
        {
            // Before proceeding further, make sure length  
            // of str2 is larger.  
            if (str1._string.Length > str2._string.Length)
            {
                string t = str1._string;
                str1._string = str2._string;
                str2._string = t;
            }

            // Take an empty string for storing result  
            string str = "";

            // Calculate length of both string  
            int n1 = str1._string.Length, n2 = str2._string.Length;
            int diff = n2 - n1;

            // Initially take carry zero  
            int carry = 0;

            // Traverse from end of both strings  
            for (int i = n1 - 1; i >= 0; i--)
            {
                // Do school mathematics, compute sum of  
                // current digits and carry  
                int sum = ((int)(str1._string[i] - '0') + (int)(str2._string[i + diff] - '0') + carry);
                str += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            // Add remaining digits of str2[]  
            for (int i = n2 - n1 - 1; i >= 0; i--)
            {
                int sum = ((int)(str2._string[i] - '0') + carry);
                str += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            // Add remaining carry  
            if (carry > 0)
                str += (char)(carry + '0');

            // reverse resultant string 
            char[] ch2 = str.ToCharArray();
            Array.Reverse(ch2);
            return new StringMath(new string(ch2));
        }

        public static StringMath operator -(StringMath str1, StringMath str2)
        {
            // Before proceeding further, 
            // make sure str1 is not smaller 
            if (IsSmaller(str1._string, str2._string))
            {
                string t = str1._string;
                str1._string = str2._string;
                str2._string = t;
            }

            // Take an empty string for  
            // storing result 
            String str = "";

            // Calculate lengths of both string 
            int n1 = str1._string.Length, n2 = str2._string.Length;
            int diff = n1 - n2;

            // Initially take carry zero 
            int carry = 0;

            // Traverse from end of both strings 
            for (int i = n2 - 1; i >= 0; i--)
            {
                // Do school mathematics, compute  
                // difference of current digits and carry 
                int sub = (((int)str1._string[i + diff] - (int)'0') -
                           ((int)str2._string[i] - (int)'0') - carry);
                if (sub < 0)
                {
                    sub = sub + 10;
                    carry = 1;
                }
                else
                    carry = 0;

                str += sub.ToString();
            }

            // subtract remaining digits of str1[] 
            for (int i = n1 - n2 - 1; i >= 0; i--)
            {
                if (str1._string[i] == '0' && carry > 0)
                {
                    str += "9";
                    continue;
                }
                int sub = (((int)str1._string[i] - (int)'0') - carry);
                if (i > 0 || sub > 0) // remove preceding 0's 
                    str += sub.ToString();
                carry = 0;
            }

            // reverse resultant string 
            char[] aa = str.ToCharArray();
            Array.Reverse(aa);
            return new StringMath(new string(aa));
        }
        
        public static StringMath operator *(StringMath str1, StringMath str2)
        {
            int len1 = str1._string.Length;
            int len2 = str2._string.Length;
            if (len1 == 0 || len2 == 0)
                return new StringMath("0");

            // will keep the result number in vector  
            // in reverse order  
            int[] result = new int[len1 + len2];

            // Below two indexes are used to  
            // find positions in result.  
            int i_n1 = 0;
            int i_n2 = 0;
            int i;

            // Go from right to left in str1  
            for (i = len1 - 1; i >= 0; i--)
            {
                int carry = 0;
                int n1 = str1._string[i] - '0';

                // To shift position to left after every  
                // multipliccharAtion of a digit in str2  
                i_n2 = 0;

                // Go from right to left in str2              
                for (int j = len2 - 1; j >= 0; j--)
                {
                    // Take current digit of second number  
                    //int n2 = str2._string[j] - '0';

                    // Multiply with current digit of first number  
                    // and add result to previously stored result  
                    // charAt current position.  
                    int sum = n1 * (str2._string[j] - '0') + result[i_n1 + i_n2] + carry;

                    // Carry for next itercharAtion  
                    carry = sum / 10;

                    // Store result  
                    result[i_n1 + i_n2] = sum % 10;

                    i_n2++;
                }

                // store carry in next cell  
                if (carry > 0)
                    result[i_n1 + i_n2] += carry;

                // To shift position to left after every  
                // multipliccharAtion of a digit in str1.  
                i_n1++;
            }

            // ignore '0's from the right  
            i = result.Length - 1;
            while (i >= 0 && result[i] == 0)
                i--;

            // If all were '0's - means either both  
            // or one of str1 or str2 were '0'  
            if (i == -1)
                return new StringMath("0");

            // genercharAte the result String  
            String s = "";

            while (i >= 0)
                s += (result[i--]);

            return new StringMath(s);
        }

        //public static StringMath operator /(StringMath str1, StringMath str2)
        //{
        //    // As result can be very large store it in string  
        //    string ans = "";
        //
        //    // Find prefix of number that is larger  
        //    // than divisor.  
        //    int idx = 0;
        //    int temp = (int)(str1._string[idx] - '0');
        //    while (temp < divisor)
        //    {
        //        temp = temp * 10 + (int)(str1._string[idx + 1] - '0');
        //        idx++;
        //    }
        //    ++idx;
        //
        //    // Repeatedly divide divisor with temp. After  
        //    // every division, update temp to include one  
        //    // more digit.  
        //    while (str1._string.Length > idx)
        //    {
        //        // Store result in answer i.e. temp / divisor  
        //        ans += (char)(temp / divisor + '0');
        //
        //        // Take next digit of number 
        //        temp = (temp % divisor) * 10 + (int)(str1._string[idx] - '0');
        //        idx++;
        //    }
        //    ans += (char)(temp / divisor + '0');
        //
        //    // If divisor is greater than number  
        //    if (ans.Length == 0)
        //        return new StringMath("0");
        //
        //    // else return ans  
        //    return new StringMath(ans);
        //}

        public static StringMath operator ++(StringMath str1)
        {
            return str1 + new StringMath("1");
        }

        public static bool operator ==(StringMath str1, StringMath str2)
        {
            return str1._string == str2._string;
        }

        public static bool operator !=(StringMath str1, StringMath str2)
        {
            return str1._string != str2._string;
        }


        public static StringMath operator ^(StringMath str1, StringMath str2)
        {
            if (str1 != new StringMath("0") && str2 == new StringMath("0"))
                return new StringMath("1");

            StringMath res = str1;
            StringMath index = new StringMath("1");
            while (index != str2)
            {
                res *= str1;
                index++;
            }

            return res;
        }
    }
}