// -----------------------------------------------------------------------
// <copyright file="Interop.cs" company="Jim Borden">
// Copyright (c) Jim Borden. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace Spectre.Net.Api.Algorithms;

internal static partial class Interop
{
#if IOS
    private const string DllName = "__Internal";
#else
    private const string DllName = "libsodium";
#endif

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    internal static partial int crypto_pwhash_scryptsalsa208sha256_ll([In]byte[] secret, nuint secretSize, [In]byte[] salt, UIntPtr saltSize, ulong N, uint r, uint p,
        [Out] byte[] key, nuint keySize);
}