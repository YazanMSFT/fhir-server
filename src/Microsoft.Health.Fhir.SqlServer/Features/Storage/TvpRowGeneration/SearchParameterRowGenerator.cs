﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using Microsoft.Health.Fhir.Core.Models;
using Microsoft.Health.Fhir.SqlServer.Features.Schema.Model;

namespace Microsoft.Health.Fhir.SqlServer.Features.Storage.TvpRowGeneration
{
    internal abstract class SearchParameterRowGenerator<TSearchValue, TRow> : ITableValuedParameterRowGenerator<ResourceMetadata, TRow>
        where TRow : struct
    {
        protected SearchParameterRowGenerator(SqlServerFhirModel model)
        {
            EnsureArg.IsNotNull(model, nameof(model));
            Model = model;
        }

        protected SqlServerFhirModel Model { get; }

        public virtual IEnumerable<TRow> GenerateRows(ResourceMetadata input)
        {
            return input.GetSearchIndexEntriesByType(typeof(TSearchValue))
                .Where(v => ShouldGenerateRow(v.SearchParameter, (TSearchValue)v.Value))
                .Select(v => GenerateRow(Model.GetSearchParamId(v.SearchParameter.Url.ToString()), v.SearchParameter, (TSearchValue)v.Value))
                .Distinct();
        }

        protected virtual bool ShouldGenerateRow(SearchParameterInfo searchParameter, TSearchValue searchValue) => true;

        protected abstract TRow GenerateRow(short searchParamId, SearchParameterInfo searchParameter, TSearchValue searchValue);
    }
}
