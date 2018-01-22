namespace GoogleTranslateToken
{
    class TokenGenerator
    {
        private static A[] rl1 = new A[] { new A() { a = true, b = false, c = 10 }, new A() { a = false, b = true, c = 6 } };
        private static A[] rl2 = new A[] { new A() { a = true, b = false, c = 3 }, new A() { a = false, b = true, c = 11 }, new A() { a = true, b = false, c = 15 } };

        public static string GetToken(string a)
        {
            long b = 406398;

            for (int i = 0; i < a.Length; i++)
            {
                int c = a[i];
                if (c < 128)
                {
                    B(ref b, c);
                }
                else
                {
                    if (c < 2048)
                    {
                        B(ref b, c >> 6 | 192);
                    }
                    else
                    {
                        if ((c & 64512) == 55296 && i < a.Length - 1 && (a[i + 1] & 64512) == 56320)
                        {
                            c = 65536 + ((c & 1023) << 10) + (a[++i] & 1023);
                            B(ref b, c >> 18 | 240);
                            B(ref b, c >> 12 & 63 | 128);
                        }
                        else
                        {
                            B(ref b, c >> 12 | 224);
                        }
                        B(ref b, c >> 6 & 63 | 128);
                    }
                    B(ref b, c & 63 | 128);
                }
            }

            RL(ref b, rl2);
            b ^= 2087938574;
            if (b < 0)
                b = (b & 2147483647) + 2147483648;
            b %= 1000000;
            return $"{b}.{b ^ 406398}";
        }

        private static void B(ref long a, int b)
        {
            a += b;
            RL(ref a, rl1);
        }

        private static void RL(ref long a, A[] b)
        {
            foreach (A c in b)
            {
                long d = c.b ? ((uint)a >> c.c) : a << c.c;
                a = c.a ? ((a + d) & 4294967295) : a ^ d;
            }
        }

    }

    struct A
    {
        public bool a;
        public bool b;
        public int c;
    }
}
