using System.Text;
using System.Text.RegularExpressions;
using Meli.App.Dto;
using Meli.App.Services.Model;
using Meli.Data.Domain;

namespace Meli.Application.Services
{
    public class DNAService : IDNAService
    {
        #region Properties & Members

        private readonly IDNARepository _DNARepository;
        private readonly string MUTANT_DNA_SEQUENCES = @"A{4,}|C{4,}|G{4,}|T{4,}";

        #endregion

        #region Constructors

        public DNAService(IDNARepository DNARepository)
        {
            _DNARepository = DNARepository;
        }

        #endregion

        #region IDNAService Implementation

        public async Task AddDNA(Meli.Data.Domain.Entities.DNAEntity dna)
        {


            await _DNARepository.AddAsync(dna);
        }

        public async Task<bool> FindMutant(string[] Dna)
        {
            var isMutant = await Analyze(Dna);
            var dNAEntity = new Meli.Data.Domain.Entities.DNAEntity()
            {
                Dna = stringArrayToString(Dna),
                IsMutant = !isMutant
            };
            await AddDNA(dNAEntity);
            return !isMutant;
        }

        public async Task<IEnumerable<DNADto>> GetDNAs()
        {
            var DNAs = await _DNARepository.GetAllAsync();

            return DNAs.Select(x => new DNADto
            {
                Id = x.Id,
                Dna = x.Dna,
                IsMutant =x.IsMutant
            });
        }

        public async Task<Stats> GetStats()
        {
            var DNAs = await GetDNAs();
            var countMDNA = (double) DNAs.Where(x => x.IsMutant == false).Count();
            var countHDNA = (double) DNAs.Where(x => x.IsMutant == true).Count();
            double calcRatio = countMDNA / countHDNA;
            var stats = new Stats() 
            { 
                count_human_dna = countHDNA,
                count_mutant_dna = countMDNA,
                ratio = calcRatio
            };
            return stats;
        }

        #endregion

        #region Analyzer

        public async Task<bool> Analyze(string[] _Dna)
        {
            //find in rows
            int count = 0;
            foreach (var item in _Dna)
            {
                if (await IsMutant(item))
                    count++;
            }
            if (IsHigherThan1(count))
            {
                return true;
            }
            //find in columns
            var rotateAgain = MatrixStringToArrayString(rotate90Clockwise(ArrayStringToMatrix(_Dna)));
            foreach (var item in rotateAgain)
            {
                if (await IsMutant(item))
                    count++;
            }
            return IsHigherThan1(count);

            //find in diagonals

            var romb = await ConvertMatrixToRomb(_Dna);
            foreach (var item in (romb))
            {
                if (await IsMutant(item))
                    count++;
            }
            if (IsHigherThan1(count))
            {
                return true;
            }
            //invert the romb to find the data in another diags
            var romb2 = await ConvertMatrixToRomb(ReverseDataInRows(_Dna));
            foreach (var item in (romb2))
            {
                if (await IsMutant(item))
                    count++;
            }
            if (IsHigherThan1(count))
            {
                return true;
            }

        }
        #endregion

        #region utils
        static string[] ReverseDataInRows(string[] array)
        {
            return array.Select(y => string.Concat(y.Reverse())).ToArray();
        }
        protected bool IsHigherThan1(int n)
        {
            return n > 1;
        }
        protected async Task<bool> IsMutant(string dnaFrag)
        {
            Regex _A = new Regex(MUTANT_DNA_SEQUENCES);
            var match = _A.Match(dnaFrag);
            if (!match.Success) { return false; }
            return true;
        }
        protected async Task<IEnumerable<string>> ConvertMatrixToRomb(string[] grid)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var s in grid)
            {
                builder.Append(s);
            }
            string matrixString = builder.ToString();

            int wordLength = grid[0].Length;
            int numberOfWords = grid.Length;
            var list = new List<string>();


            if (wordLength > 0)
            {
                int[] indexes = new int[numberOfWords];
                indexes[0] = matrixString.Length - wordLength;
                for (int i = 1; i < numberOfWords; i++)
                {
                    indexes[i] = indexes[i - 1] - wordLength;
                }

                int wordCount = numberOfWords + wordLength - 1;

                for (int i = 0; i < wordCount; i++)
                {
                    builder.Remove(0, builder.Length);
                    for (int j = 0; (j <= i) && (j < numberOfWords); j++)
                    {
                        if (indexes[j] < wordLength * (wordCount - i))
                        {
                            char c = matrixString[indexes[j]];
                            builder.Append(c);
                            indexes[j]++;
                        }
                    }
                    string s = Reverse(builder.ToString());
                    if (s.Length > 3) { list.Add(s); }
                }
            }
            return list;
        }
        protected char[,] rotate90Clockwise(char[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i; j < n - i - 1; j++)
                {
                    char top = matrix[i, j];
                    //MOve left to top
                    matrix[i, j] = matrix[n - 1 - j, i];

                    //Move bottom to left
                    matrix[n - 1 - j, i] = matrix[n - i - 1, n - 1 - j];

                    //Move right to bottom
                    matrix[n - i - 1, n - 1 - j] = matrix[j, n - i - 1];

                    //Move top to right
                    matrix[j, n - i - 1] = top;
                }
            }
            return matrix;
        }
        protected string[] MatrixStringToArrayString(char[,] matrix)
        {
            var array = new string[matrix.GetLength(0)];
            var N = array.Length;

            for (int i = 0; i < N; i++)
            {
                var seq = String.Empty;
                for (int j = 0; j < N; j++)
                {
                    seq = seq + matrix[i, j];
                }
                array[i] = seq;

            }
            return array;
        }
        protected char[,] ArrayStringToMatrix(string[] array)
        {
            var N = array.Length;
            var matrix = new char[N, N];
            var aux = 0;
            foreach (var item in array)
            {
                var arrayAux = StringToCharArray(item);
                for (int i = 0; i < item.Length; i++)
                {
                    matrix[aux, i] = arrayAux[i];
                }
                aux++;
            }
            return matrix;

        }
        protected char[] StringToCharArray(string str)
        {
            // Creating array of string length 
            char[] ch = new char[str.Length];

            // Copy character by character into array 
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            return ch;
        }
        protected string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        protected string stringArrayToString(string[] array)
        {
            string str = String.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in array)
            {
                stringBuilder.Append(item.ToString() + ",");
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}