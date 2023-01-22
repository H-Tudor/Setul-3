using System;
using System.Collections.Generic;

namespace Setul_3 {
	internal class Program {
		static void Main(string[] args) {
			int n;
			bool ok = true;

			while(ok) { 
				n = IntGet("ex. id");
				switch(n) {
					case 1:
						Console.WriteLine($"Sum: {ArraySum(ArrayGet())}");
						break;
					case 2:
						Console.WriteLine($"First Occurence: {ArrayFirstOccurence(ArrayGet(), IntGet("position"))}");
						break;
					case 3:
						int[] p = ArrayMinMaxPositions(ArrayGet());
						Console.WriteLine($"Min: {p[0]}, Max: {p[1]}");
						break;
					case 4:
						int[,] po = ArrayMinMaxOccurence(ArrayGet());
						Console.WriteLine($"Min: {po[0,0]} x {po[1, 0]}, Max: {po[1, 0]} x {po[1, 1]}");
						break;
					case 5:
						ArrayPrintConsole(ArrayInsert(ArrayGet(), IntGet("value"), IntGet("position")));
						break;
					case 6:
						ArrayPrintConsole(ArrayDelete(ArrayGet(), IntGet("position")));
						break;
					case 7:
						ArrayPrintConsole(ArrayReverse(ArrayGet()));
						break;
					case 8:
						ArrayPrintConsole(ArrayShiftLeft(ArrayGet(), 1));
						break;
					case 9:
						ArrayPrintConsole(ArrayShiftLeft(ArrayGet(), IntGet("positions")));
						break;
					case 10:
						Console.WriteLine(ArrayBinarySearch(ArrayGet(), IntGet("k")));
						break;
					case 11:
						ArrayPrintConsole(EratostenesChirus(IntGet()));
						break;
					case 12:
						ArrayPrintConsole(ArraySelectionSort(ArrayGet()));
						break;
					case 13:
						ArrayPrintConsole(ArrayInsertionSort(ArrayGet()));
						break;
					case 14:
						ArrayPrintConsole(ArrayMoveZerosToEnd(ArrayGet()));
						break;
					case 15:
						ArrayPrintConsole(ArrayRemoveDuplicates(ArrayGet()));
						break;
					case 16:
						Console.WriteLine($"CMMDC: {ArrayGreatestCommonDivisor(ArrayGet())}");
						break;
					case 17:
						Console.WriteLine(ConvertFromTen(GetNumberToConvert(), GetConversionToBase()));
						break;
					case 18:
						Console.WriteLine(ArrayPolynomial(ArrayGet(), IntGet()));
						break;
					case 19:
						Console.WriteLine(KMPSearch(ArrayGet("source"), ArrayGet("pattern")));
						break;
					case 20:
						Console.WriteLine($"Overlaps: {ArrayShiftingCompare(ArrayGet(), ArrayGet())}");
						break;
					case 21:
						ArrayPrintConsole(ArrayCompare(ArrayGet("first array"), ArrayGet("second array")));
						break;
					case 22:
						int[] a = ArrayGet("first array");
						int[] b = ArrayGet("second array");

						ArrayPrintConsole(ArrayIntersect(a, b));
						ArrayPrintConsole(ArrayReunion(a, b));
						ArrayPrintConsole(ArrayDiff(a, b));
						ArrayPrintConsole(ArrayDiff(b, a));
						break;
					case 23:
						//TODO: Implement intersect, reunion & diff1 / diff2 for orderd array
						break;
					case 24:
						//TODO: Implement intersect, reunion & diff1 / diff2 for binary array
						break;
					case 25:
						ArrayPrintConsole(ArrayMerge(ArrayGet("first array"), ArrayGet("second array")));
						break;
					case 26:
						Console.WriteLine("Vezi proiect numere mari / Big number");
						throw new NotImplementedException();
					case 27:
						Console.WriteLine(ArrayAfterSortPosition(ArrayGet(), IntGet()));
						break;
					case 28:
						ArrayPrintConsole(ArrayQuickSort(ArrayGet()));
						break;
					case 29:
						ArrayPrintConsole(ArrayMergeSort(ArrayGet()));
						break;
					case 30:
						ArrayPrintConsole(ArrayTwoCriteriaSort(ArrayGet("array to sort"), ArrayGet("array of weights")));
						break;
					case 31:
						Console.WriteLine(ArrayMajorityValue(ArrayGet()));
						break;
					default:
						Console.WriteLine("Incorrect ex. number");
						ok = false;
						break;
				}
			}

			Console.ReadKey();
		}

		static int ArrayMajorityValue(int[] array) {
			int majorityThreshold = (int)Math.Ceiling((float)array.Length / 2);
			Dictionary<int, int> votes = new Dictionary<int, int>();

			for(int i = 0; i < array.Length; i++) {
				votes[array[i]]++;
				if(votes[array[i]] == majorityThreshold)
					return array[i];
			}

			return int.MinValue;
		}

		/*
		 Two Criteria Sort: Normal sort until you have two consecutive equal numbers, then compare weights, also, when switching elements, switch the weights too		 
		 */

		static int[] ArrayTwoCriteriaSort(int[] array, int[] weights) {
			bool ok;			
			do {
				ok = true;
				for(int i = 0; i < array.Length - 1; i++) {
					if(array[i] > array[i+1]) {
						ok = false;
						(array[i], array[i + 1]) = (array[i + 1], array[i]);
						(weights[i], weights[i + 1]) = (weights[i + 1], weights[i]);
					} else if(array[i] == array[i+1]) {
						if(weights[i] > weights[i + 1]) {
							ok = false;
							(array[i], array[i + 1]) = (array[i + 1], array[i]);
							(weights[i], weights[i + 1]) = (weights[i + 1], weights[i]);
						}
					}
				}
			} while(!ok);
			return array;
		}

		/*
			1. Check if array.length is 1; if so, return the array
			2. Determine the half-point
			3. Create 2 new arrays of array.length / 2 (ceil first, floor last) and copy the content of the original array in the apropriate positions
			4. For each of them call (recursive) the above code
			5. Merge the 2 resulting arrays.
		 */

		static int[] ArrayMergeSort(int[] array) {
			if(array.Length == 1)
				return array;

			int halfLength = array.Length / 2;

			int[] a = new int[halfLength + 1];
			int[] b = new int[halfLength / 2];

			for(int i = 0; i <= halfLength / 2; i++)
				a[i] = array[i];

			for(int i = 0; i < halfLength; i++) {
				b[i] = array[i + halfLength + 1];
			}

			a = ArrayMergeSort(a);
			b = ArrayMergeSort(b);

			return ArrayMerge(a, b);
		}


		/*
		1. Choose "random" pivot index
		2. Swap the the element at the pivot position with the last element
		3. Search for "itemFromLeft" (IFL) (0 -> IFR index), an item larger that the pivot element
		4. Search for "itemFromRight" (IFR) (n - 1 -> IFL index), an item smaller than the pivot element
		5. Partition the Array in pre-pivot index and after-pivot index 
		 */

		static int[] ArrayQuickSort(int[] array) {
			ArrayQuickSort(array, 0, array.Length - 1);
			return array;
		}

		static void ArrayQuickSort(int[] array, int low, int high) {
			if(low < high) {
				int pi = ArrayQuickSortPartition(array, low, high);
				ArrayQuickSort(array, low, pi - 1);
				ArrayQuickSort(array, pi + 1, high);
			}
		}

		static int ArrayQuickSortPartition(int[] array, int low, int high) {
			int pivot = array[high];
			int i = low - 1;

			for(int j = low; j < high; j++) {
				if(array[j] < pivot) {
					i++;
					(array[j], array[i]) = (array[i], array[j]);
				}
			}

			(array[high], array[i + 1]) = (array[i + 1], array[high]);
			return (i + 1);
		}

		static int ArrayAfterSortPosition(int[] array, int index) {
			int value = array[index];
			int counter = 0;

			for(int i = 0; i < array.Length; i++) {
				if(array[i] < array[index])
					counter++;
			}

			return counter;
		}

		static int[] ArrayMerge(int[] a, int[] b) {
			int[] c = new int[a.Length + b.Length];

			if(a.Length + b.Length == 2) {
				if(a[0] < b[0]) {
					c[0] = a[0];
					c[1] = b[0];
				} else {
					c[0] = a[0];
					c[1] = b[0];
				}
			} else {
				int i = 0, j = 0, k = 0;
				while(i < a.Length && j < b.Length) {
					if(a[i] < b[i]) {
						c[k] = a[i];
						i++;
					} else {
						c[k] = b[j];
						j++;
					}
					k++;
				}

				while(i < a.Length) {
					c[k] = a[i];
					k++;
					i++;
				}

				while(j < b.Length) {
					c[k] = b[j];
					k++;
					j++;
				}
			}

			return c;
		}

		//static int[] ArrayOrderedReunion(int[] a, int[] b) {
		//	a = ArrayRemoveDuplicates(a);
		//	b = ArrayRemoveDuplicates(b);

		//}

		//static int[] ArrayOrderedIntersection(int[] a, int[] b) {
		//	a = ArrayRemoveDuplicates(a);
		//	b = ArrayRemoveDuplicates(b);

		//	int start = Math.Max(a[0], b[0]);
		//	bool startPosToggle = start == a[0];
		//	int startPos = startPosToggle ? ArrayBinarySearch(b, a[0]) : ArrayBinarySearch(a, b[0]);

		//	int endPos = Math.Min(a.Length, b.Length);
		//	bool endToggle = a.Length == endPos;

		//	if(start == -1)
		//		return Array.Empty<int>();

		//	int[] array = new int[endPos];
		//	int i = startPosToggle ? startPos : 0, j = startPosToggle ? 0 : startPos, k = 0;

		//	while(i < a.Length && j < b.Length && k < array.Length) {
		//		if(a[i] == b[j]) {
		//			array[k] = a[i];
		//			i++; j++; k++;
		//		} else {

		//		}
		//	}

		//	return array;
		//}

		static int[] ArrayOrderedDiff(int[] a, int[] b) {
			a = ArrayRemoveDuplicates(a);
			b = ArrayRemoveDuplicates(b);

			if(a.Length <= b.Length)
				return new int[0];

			int k = 0;
			int newLength = a.Length - b.Length;
			int[] array = new int[newLength];

			for(int i = a.Length; i >= b.Length; i--) {
				array[k] = a[i];
				k++;
			}

			array = ArrayRemoveDuplicates(array);
			return array;
		}

		static int[] ArrayDiff(int[] a, int[] b) {
			a = ArrayRemoveDuplicates(a);
			b = ArrayRemoveDuplicates(b);
			int[] c = new int[a.Length];
			int k = 0;

			for(int i = 0; i < a.Length; i++) {
				for(int j = 0; i < b.Length; i++) {
					if(a[i] != b[j] && ArrayBinarySearch(c, a[i]) == -1) {
						c[k] = a[i];
						k++;
						break;
					}
				}
			}

			return c;
		}

		static int[] ArrayReunion(int[] a, int[] b) {
			a = ArrayRemoveDuplicates(a);
			b = ArrayRemoveDuplicates(b);
			int maxLength = Math.Max(a.Length, b.Length);
			int[] c = new int[maxLength];

			int i;
			for(i = 0; i < a.Length; i++) {
				c[i] = a[i];
			}

			for(int j = 0; j < b.Length; j++) {
				if(ArrayBinarySearch(c, b[j]) == -1) {
					c[i + j] = b[j];
				}
			}

			return c;
		}

		static int[] ArrayIntersect(int[] a, int[] b) {
			a = ArrayRemoveDuplicates(a);
			b = ArrayRemoveDuplicates(b);
			int minLength = Math.Min(a.Length, b.Length);
			int[] c = new int[minLength];
			int k = 0;

			if(a.Length == minLength) {
				for(int i = 0; i < a.Length; i++) {
					for(int j = 0; j < b.Length; j++) {
						if(a[i] == b[j]) {
							c[k] = a[i];
							k++;
							break;
						}
					}
				}
			} else {
				for(int i = 0; i < b.Length; i++) {
					for(int j = 0; j < a.Length; j++) {
						if(b[i] == a[j]) {
							c[k] = b[i];
							k++;
							break;
						}
					}
				}
			}

			return c;
		}

		static int[] ArrayCompare(int[] a, int[] b) {
			int minLength = Math.Min(a.Length, b.Length);
			for(int i = 0; i < minLength; i++) {
				if(a[i] < b[i])
					return a;
				else if(a[i] > b[i])
					return b;
			}

			return minLength == a.Length ? a : b;
		}

		static int ArrayShiftingCompare(int[] a, int[] b) {
			if(a.Length != b.Length)
				return 0;

			int count = 0;

			for(int i = 0; i < b.Length; i++) {
				for(int j = 0; j < a.Length; j++) {
					if(a[j] == b[j])
						count++;
				}
				b = ArrayShiftLeft(b, 1);
			}

			return count;
		}

		static int KMPSearch(int[] txt, int[] pat) {
			int numberOfOccurences = 0;

			int M = pat.Length;
			int N = txt.Length;

			int[] lps = new int[M];
			int j = 0;

			ComputeLPSArray(pat, M, lps);

			int i = 0;
			while((N - i) >= (M - j)) {
				if(pat[j] == txt[i]) {
					j++;
					i++;
				}
				if(j == M) {
					numberOfOccurences++;
					j = lps[j - 1];
				} else if(i < N && pat[j] != txt[i]) {
					if(j != 0)
						j = lps[j - 1];
					else
						i = i + 1;
				}
			}

			return numberOfOccurences;
		}

		static void ComputeLPSArray(int[] pat, int M, int[] lps) {
			int len = 0;
			int i = 1;
			lps[0] = 0;

			while(i < M) {
				if(pat[i] == pat[len]) {
					len++;
					lps[i] = len;
					i++;
				} else {
					if(len != 0) {
						len = lps[len - 1];
					} else {
						lps[i] = len;
						i++;
					}
				}
			}
		}

		static double ArrayPolynomial(int[] coefs, int x) {
			double result = 0;

			for(int i = 0; i < coefs.Length; i++) {
				result += Math.Pow(x, i) * coefs[i];
			}

			return result;
		}

		static int GetConversionToBase() {
			Console.WriteLine("Enter base to convert from base 10: ");
			return int.Parse(Console.ReadLine().Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
		}

		static string[] GetNumberToConvert() {
			Console.WriteLine("Enter number in base 10: ");
			return Console.ReadLine().Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		}

		static string ConvertFromTen(string[] numberParts, int toBase) {
			if(toBase == 10) {
				return numberParts.Length > 1 ? $"{numberParts[0]}.{numberParts[1]}" : $"{numberParts[0]}";
			}

			char[] pool = "0123456789ABCDEF".ToCharArray();

			Stack<char> IntParts = new Stack<char>();
			int IntNumber = int.Parse(numberParts[0]);
			double FractionPart = numberParts.Length > 1 && numberParts[1] != String.Empty ? int.Parse(numberParts[1]) * Math.Pow((double)10, (double)(-1 * numberParts[1].Length)) : 0;
			string outputNumber = "";

			while(IntNumber != 0) {
				char current = pool[IntNumber % toBase];
				IntParts.Push(current);
				IntNumber /= toBase;
			}

			while(IntParts.Count > 0) {
				outputNumber += IntParts.Pop();
			}

			if(FractionPart != 0) {
				outputNumber += '.';

				while(FractionPart != 0) {
					outputNumber += Math.Floor(FractionPart * toBase);
					FractionPart = (FractionPart * toBase) - Math.Floor(FractionPart * toBase);
				}
			}

			return outputNumber;
		}

		static int ArrayGreatestCommonDivisor(int[] array) {
			array = ArrayMoveZerosToEnd(array);

			int zeroPosition = ArrayBinarySearch(array, 0);
			if(zeroPosition != -1)
				return 0;

			int GCD = GreatestCommonDivisor(array[0], array[1]);
			for(int i = 2; i < zeroPosition; i++) {
				GCD = GreatestCommonDivisor(array[i], GCD);
			}

			return GCD;
		}

		static int GreatestCommonDivisor(int a, int b) {
			int c;

			while(b != 0) {
				c = a % b;
				a = b;
				b = c;
			}

			return a;
		}

		static int[] ArrayRemoveDuplicates(int[] array) {
			array = ArrayInsertionSort(array);
			int i = 1, last = array[0];

			while(i < array.Length) {
				while(array[i] == last) {
					array = ArrayDelete(array, i);
				}
				last = array[i];
				i++;
			}

			return array;
		}

		static int[] ArrayMoveZerosToEnd(int[] array) {
			int k = array.Length - 1;

			for(int i = 0; i <= k; i++) {
				if(array[i] == 0) {
					(array[i], array[k]) = (array[k], array[i]);
					k--;
				}
			}

			return array;
		}

		static int[] ArrayInsertionSort(int[] array) {
			for(int i = 1; i < array.Length; i++) {
				for(int j = i; j > 1 && array[j] < array[j - 1]; j--) {
					(array[j], array[j - 1]) = (array[j - 1], array[j]);
				}
			}

			return array;
		}

		static int[] ArraySelectionSort(int[] array) {
			int k;

			for(int i = 0; i < array.Length; i++) {
				k = i;

				for(int j = i + 1; i < array.Length; i++) {
					if(array[j] < array[k])
						k = j;
				}

				(array[i], array[k]) = (array[k], array[i]);
			}

			return array;
		}

		static int[] EratostenesChirus(int n) {
			int[] array = new int[n + 1];
			int counter =  0;

			for(int i = 2; i < n + 1; i++) {
				for(int j = i + 1; j < n + 2; j++) {
					if(j % i == 0)
						array[i]++;
				}
			}

			List<int> primes = new List<int>();
			for(int i = 0; i < n; i++) {
				if(array[i] == 0) {
					primes.Add(array[i]);
				}
			}

			return primes.ToArray();
		}

		static int ArrayBinarySearch(int[] array, int value) {
			int start = 0, end = array.Length - 1, middle = (start + end) / 2;

			while(start < end) {
				if(value == array[middle])
					return middle;
				else if(value > array[middle])
					start = middle;
				else if(value < array[middle])
					end = middle;
				middle = (start + end) / 2;
			}

			return -1;
		}

		static int[] ArrayShiftLeft(int[] array, int k) {
			int[] p = new int[k];

			for(int i = 0; i < array.Length; i++) {
				if(i < k) {
					p[i] = array[i];
					array[i] = array[i + k];
				} else if(i < array.Length - k) {
					array[i] = array[i + k];
				} else {
					array[i] = p[k + i - array.Length];
				}
			}

			return array;
		}

		static int[] ArrayReverse(int[] array) {
			for(int i = 0; i < array.Length / 2; i++) {
				(array[i], array[array.Length - i]) = (array[array.Length - i], array[i]);
			}

			return array;
		}

		static int[] ArrayDelete(int[] array, int position) {
			int[] p = new int[array.Length - 1];
			for(int i = 0; i < array.Length - 1; i++) {
				p[i] = i < position ? array[i] : array[i + 1];
			}
			return p;
		}

		static int[] ArrayInsert(int[] array, int value, int position) {
			int[] p = new int[array.Length + 1];
			p[position] = value;

			for(int i = 0; i < array.Length + 1; i++) {
				p[i] = i < position ? array[i] : i > position ? array[i + 1] : value;
			}

			return p;
		}

		static int[,] ArrayMinMaxOccurence(int[] array) {
			int[,] p = new int[2, 2] { { int.MaxValue, int.MinValue }, { 0, 0 } };
			Dictionary<int, int> occurence = new Dictionary<int, int>();

			for(int i = 0; i < array.Length; i++) {
				occurence[array[i]]++;

				if(array[i] > p[0, 0])
					p[0, 0] = array[i];
				else if(array[i] < p[0, 1])
					p[0, 1] = array[i];
			}

			p[1, 0] = occurence[p[0, 0]];
			p[1, 1] = occurence[p[0, 1]];

			return p;
		}

		static int[] ArrayMinMaxPositions(int[] array) {
			int[] p = new int[2] { int.MaxValue, int.MinValue };

			for(int i = 0; i < array.Length; i++) {
				if(array[i] > p[0])
					p[0] = array[i];
				else if(array[i] < p[1])
					p[1] = array[i];
			}

			return p;
		}

		static int ArrayFirstOccurence(int[] array, int k) {
			for(int i = 0; i < array.Length; i++)
				if(array[i] == k)
					return k;

			return -1;
		}

		static int ArraySum(int[] array) {
			int sum = 0;

			for(int i = 0; i < array.Length; i++) {
				sum += array[i];
			}

			return sum;
		}

		static void ArrayPrintConsole(int[] array) {
			for(int i = 0; i < array.Length; i++)
				Console.Write($"{array[i]} ");
			Console.WriteLine();
		}

		static int IntGet(string displayText = "number") {
			int n;
			Console.WriteLine($"Enter {displayText}: ");
			if(int.TryParse(Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0], out n) == false)
				throw new Exception("Bad Input");

			return n;
		}

		static int[] ArrayGet(string displayText = "array", bool requireN = false) {
			int n = 0;
			int[] array;
			string[] buffer;

			if(requireN == true) {
				Console.WriteLine("Enter n: ");
				if(int.TryParse(Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0], out n) == false)
					throw new Exception("Bad Input");
			}

			Console.WriteLine($"Enter {displayText}: ");
			buffer = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			if(requireN == true) {
				n = Math.Abs(n);
				if(buffer.Length != n)
					throw new Exception("Number of input elements differs from declared elements");
			} else {
				n = buffer.Length;
			}

			array = new int[n];
			for(int i = 0; i < n; i++) {
				if(int.TryParse(buffer[i], out array[i]) == false)
					throw new Exception("Bad Input");
			}

			return array;
		}

	}
}
