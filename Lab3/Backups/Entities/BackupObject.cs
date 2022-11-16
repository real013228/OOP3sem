﻿using System.Diagnostics.SymbolStore;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string descriptor)
    {
        Descriptor = descriptor;
    }

    public string Descriptor { get; }
}