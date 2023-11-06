    // method line 2 FARTCITY exercise 9.1
    .method public static hidebysig 
           default void SelectionSort (int32[] arr)  cil managed 
    {
        // Method begins at RVA 0x20b4
	// Code size 57 (0x39)
	.maxstack 4
	.locals init (
		int32	V_0, 		|  i
		int32	V_1,		|  least
		int32	V_2,		|	
		int32	V_3)		|
	IL_0000:  ldc.i4.0 		| push constant 0
	IL_0001:  stloc.0 		| Store 0 at varibale i (i = 0)
	IL_0002:  br.s IL_0032 	| unconditional jump to IL_0032

	IL_0004:  ldloc.0 		| Load local variable 0/i
	IL_0005:  stloc.1 		| Store at variable 1/ least value i (least = i)
	IL_0006:  ldloc.0 		| Load variable 0/i
	IL_0007:  ldc.i4.1 		| Push constant 1
	IL_0008:  add 			| Add 1 to varibale 0/i (i+1)
	IL_0009:  stloc.3 		| Store i+1 at varable 3
	IL_000a:  br.s IL_001a	| unconditional jump to IL_001a 

	IL_000c:  ldarg.0 		| Load argument 0/arr
	IL_000d:  ldloc.3 		| Load local var 3/j
	IL_000e:  ldelem.i4 	| Local array element at index j (arr[j])
	IL_000f:  ldarg.0 		| Load argument 0/arr 
	IL_0010:  ldloc.1 		| Load local var 1/least
	IL_0011:  ldelem.i4 	| Local array element at index least (arr[least])
	IL_0012:  bge.s IL_0016	| Branch if greater than or equal IL_0016 (arr[j] >= arr[least])

	IL_0014:  ldloc.3 		| Load local var 3/j
	IL_0015:  stloc.1 		| Store j at variable least (least = j)
	
	IL_0016:  ldloc.3 		| Load local var 3/j
	IL_0017:  ldc.i4.1 		| push constant 1
	IL_0018:  add 			| Add 1 to varibale 1/j (j++)
	IL_0019:  stloc.3 		| Store j++
	
	IL_001a:  ldloc.3		| Load local variable 3/i+1
	IL_001b:  ldarg.0		| Load argument 0/arr
	IL_001c:  ldlen 		| Load length of arr
	IL_001d:  conv.i4 		| Conevert length to int
	IL_001e:  blt.s IL_000c | Branch if less than to IL_000c. if i+1 < arr.length

	IL_0020:  ldarg.0 		| Load arg 0/arr
	IL_0021:  ldloc.0 		| Load var 0/i
	IL_0022:  ldelem.i4 	| Local array element at index i (arr[i])
	IL_0023:  stloc.2 		| Store arr[i] variable 2/tmp (tmp = arr[i])
	IL_0024:  ldarg.0 		| Load arg 0/arr
	IL_0025:  ldloc.0 		| Load var 0/i Load the index (0/i) onto the stack
	IL_0026:  ldarg.0 		| Load arg 0/arr
	IL_0027:  ldloc.1 		| Load the index (1/least) onto the stack
	IL_0028:  ldelem.i4 	| Local array element at index least (arr[least])
	IL_0029:  stelem.i4 	| Store element 1/least at index 0/i (arr[i] = arr[least])
	IL_002a:  ldarg.0 		| Load arg 0/arr
	IL_002b:  ldloc.1 		| Load the index (1/least) onto the stack
	IL_002c:  ldloc.2 		| Load var 2/tmp
	IL_002d:  stelem.i4 	| Store element 2/tmp at index 1/least (arr[least] = tmp)
	IL_002e:  ldloc.0 		| Load var 0/i
	IL_002f:  ldc.i4.1 		| push constant 1
	IL_0030:  add 			| Add 1 to varibale 1/i (i++) 
	IL_0031:  stloc.0 		| Store i++

	IL_0032:  ldloc.0		| Load local variable 0/i
	IL_0033:  ldarg.0 		| Load locale variable arr
	IL_0034:  ldlen 		| load length of array
	IL_0035:  conv.i4 		| Converter length to an integer 
	IL_0036:  blt.s IL_0004 | Branch if less than. Compares i with length of array. If less jump to 0004. (i < arr.Length)

	IL_0038:  ret 			| Return
    } // end of method Selsort::SelectionSort

    