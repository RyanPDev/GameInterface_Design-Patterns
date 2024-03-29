﻿using System;
using System.Collections.Generic;

public class UseCase : IDisposable
{
    protected List<IDisposable> _disposables = new List<IDisposable>();

    public virtual void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
