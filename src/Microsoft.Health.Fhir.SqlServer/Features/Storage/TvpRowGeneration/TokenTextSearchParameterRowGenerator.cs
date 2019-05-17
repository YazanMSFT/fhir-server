﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;
using Microsoft.Health.Fhir.Core.Models;
using Microsoft.Health.Fhir.SqlServer.Features.Schema.Model;

namespace Microsoft.Health.Fhir.SqlServer.Features.Storage.TvpRowGeneration
{
    internal class TokenTextSearchParameterRowGenerator : SearchParameterRowGenerator<TokenSearchValue, V1.TokenTextTableTypeRow>
    {
        public TokenTextSearchParameterRowGenerator(SqlServerFhirModel model)
            : base(model)
        {
        }

        protected override bool ShouldGenerateRow(SearchParameterInfo searchParameter, TokenSearchValue searchValue)
        {
            return !string.IsNullOrWhiteSpace(searchValue.Text);
        }

        protected override V1.TokenTextTableTypeRow GenerateRow(short searchParamId, SearchParameterInfo searchParameter, TokenSearchValue searchValue)
        {
            return new V1.TokenTextTableTypeRow(searchParamId, searchValue.Text);
        }
    }
}
