namespace Ilitera.Common
{
    using System;
    using System.Collections;
    using System.Text;

    [Flags]
    public enum PwdMaskFlags : byte {lower = 1, upper = 2, digit = 4, symbol = 8, any = 16, banco = 32 };

    public class PasswordGenerator
    {
        public const int LBoundLower    = 0;
        public const int UBoundLower    = 25;
        public const int LBoundUpper    = 26;//
        public const int UBoundUpper    = 51;
        public const int LBoundDigit    = 52;
        public const int UBoundDigit    = 61;
        public const int LBoundSymbol   = 62;
        public const int UBoundSymbol   = 94;
        public const int LBoundAny      = 0;
        public const int UBoundAny      = 94;

        public const int DefaultMinimum = 3;
        public const int DefaultMaximum = 3;

        public PasswordGenerator() 
        {
            this.Minimum 					= DefaultMinimum;
            this.Maximum 					= DefaultMaximum;
            this.FirstCharacter 			= PwdMaskFlags.banco;
            this.LastCharacter 				= PwdMaskFlags.banco;
            this.ConsecutiveCharacters 		= false;
            this.RepeatCharacters 			= true;
            this.Exclusions                 = null;
            randomNumber = new Random();
        }		
		
        protected char GetRandomCharacter(PwdMaskFlags chartype)
        {
            int upperBound, lowerBound;
            switch ( chartype )
            {
				case PwdMaskFlags.banco:
				{
					lowerBound = PasswordGenerator.LBoundUpper;
					upperBound = PasswordGenerator.UBoundDigit;
				}
					  break;
                case PwdMaskFlags.lower:
                {
                    lowerBound = PasswordGenerator.LBoundLower;
                    upperBound = PasswordGenerator.UBoundLower;
                }
                    break;
                case PwdMaskFlags.upper:
                {
                    lowerBound = PasswordGenerator.LBoundUpper;
                    upperBound = PasswordGenerator.UBoundUpper;
                }
                    break;
                case PwdMaskFlags.digit:
                {
                    lowerBound = PasswordGenerator.LBoundDigit;
                    upperBound = PasswordGenerator.UBoundDigit;
                }
                    break;
                case PwdMaskFlags.symbol:
                {
                    lowerBound = PasswordGenerator.LBoundSymbol;
                    upperBound = PasswordGenerator.UBoundSymbol;
                }
                    break;
                case PwdMaskFlags.any:
                {
                    lowerBound = PasswordGenerator.LBoundAny;
                    upperBound = PasswordGenerator.UBoundAny;
                }
                    break;
                default:
                {
                    lowerBound = PasswordGenerator.LBoundAny;
                    upperBound = PasswordGenerator.UBoundAny;
                }
                    break;
            }
          char randomChar = pwdCharArray[randomNumber.Next(lowerBound, upperBound)];
//			char randomChar = pwdCharArray[randomNumber.Next(26, 61)];
            return randomChar;
        }
        
        public string Password
        {
            get
            {  
                // Pick random length between minimum and maximum
                int pwdLength = randomNumber.Next(this.Minimum, ( this.Maximum ));

                StringBuilder pwdBuffer = new StringBuilder();
                pwdBuffer.Capacity = this.Maximum;

                // Generate random characters
                char lastCharacter, nextCharacter;

                // Initial dummy character flag
                lastCharacter = nextCharacter = '\n';

                for ( int i = 0; i < pwdLength; i++ )
                {
                    bool hasExclusions = true;
                    PwdMaskFlags charFlag = PwdMaskFlags.banco;

                    if ( 0 == i )
                    {
                        if ( PwdMaskFlags.banco != this.firstCharacter )
                        {
                            charFlag = this.firstCharacter;
                            // FirstCharacter property overrides exclusion
                            hasExclusions = false;
                        }
                    }    

                    if ( (pwdLength - 1) == i )
                    {
                        if ( PwdMaskFlags.banco != this.lastCharacter )
                        {
                            charFlag = this.lastCharacter;
                            // LastCharacter property overrides exclusion
                            hasExclusions = false;

                        }
                    }   

                    nextCharacter = GetRandomCharacter(charFlag);
  
                    if ( false == this.ConsecutiveCharacters )
                    {
                        while ( lastCharacter == nextCharacter )
                        {
                            nextCharacter = GetRandomCharacter(charFlag);
                        }
                    }

                    if ( false == this.RepeatCharacters )
                    {
                        string temp = pwdBuffer.ToString();
                        int duplicateIndex = temp.IndexOf(nextCharacter);
                        while ( -1 != duplicateIndex )
                        {
                            nextCharacter = GetRandomCharacter(charFlag);
                            duplicateIndex = temp.IndexOf(nextCharacter);
                        }
                    }

                    if ( ( null != this.Exclusions )&& (true == hasExclusions) )
                    {
                        while ( -1 != this.Exclusions.ToString().IndexOf(nextCharacter) )
                        {
                            nextCharacter = GetRandomCharacter(charFlag);
                        }                          
                    }

                    pwdBuffer.Append(nextCharacter);
                    lastCharacter = nextCharacter;
                }

                if ( null != pwdBuffer )
                {
                    return pwdBuffer.ToString();
                }
                else
                {
                    return String.Empty;
                }	
            }
        }
            
        public PwdMaskFlags FirstCharacter
        {
            get { return this.firstCharacter; }
            set	{ this.firstCharacter = value;}
        }

        public PwdMaskFlags LastCharacter
        {
            get { return this.lastCharacter; }
            set	{ this.lastCharacter = value;}
        }

        public char[] Exclusions
        {
            get { return this.exclusionSet;  }
            set { this.exclusionSet = value; }
        }

        public int Minimum
        {
            get { return this.minSize; }
            set	
            { 
                this.minSize = value;
                if ( PasswordGenerator.DefaultMinimum > this.minSize )
                {
                    this.minSize = PasswordGenerator.DefaultMinimum;
                }
            }
        }

        public int Maximum
        {
            get { return this.maxSize; }
            set	
            { 
                this.maxSize = value;
                if ( this.minSize >= this.maxSize )
                {
                    this.maxSize = PasswordGenerator.DefaultMaximum;
                }
            }
        }

        public bool RepeatCharacters
        {
            get { return this.hasRepeating; }
            set	{ this.hasRepeating = value;}
        }

        public bool ConsecutiveCharacters
        {
            get { return this.hasConsecutive; }
            set	{ this.hasConsecutive = value;}
        }

        private int 			minSize;
        private int 			maxSize;
        private bool			hasRepeating;
        private bool			hasConsecutive;
        private PwdMaskFlags	firstCharacter;
        private PwdMaskFlags	lastCharacter;
        private Random 			randomNumber;
        private char[]          exclusionSet;
		private char[]			pwdCharArray = {
												   'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
												   'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
												   '0','1','2','3','4','5','6','7','8','9',
												   '`','~','!','@','#','$','%','^','&','*','(',')','-','_','=','+','[',']','{','}','\\','|',';',':','\'','"',',','<','.','>','/','?'
											   };
    }
}