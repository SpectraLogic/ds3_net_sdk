/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

// This code is auto-generated, do not modify
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class ModifyDataPersistenceRuleSpectraS3Request : Ds3Request
    {
        
        public string DataPersistenceRule { get; private set; }

        
        private DataIsolationLevel? _isolationLevel;
        public DataIsolationLevel? IsolationLevel
        {
            get { return _isolationLevel; }
            set { WithIsolationLevel(value); }
        }

        private int? _minimumDaysToRetain;
        public int? MinimumDaysToRetain
        {
            get { return _minimumDaysToRetain; }
            set { WithMinimumDaysToRetain(value); }
        }

        private DataPersistenceRuleType? _type;
        public DataPersistenceRuleType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        public ModifyDataPersistenceRuleSpectraS3Request WithIsolationLevel(DataIsolationLevel? isolationLevel)
        {
            this._isolationLevel = isolationLevel;
            if (isolationLevel != null) {
                this.QueryParams.Add("isolation_level", isolationLevel.ToString());
            }
            else
            {
                this.QueryParams.Remove("isolation_level");
            }
            return this;
        }
        public ModifyDataPersistenceRuleSpectraS3Request WithMinimumDaysToRetain(int? minimumDaysToRetain)
        {
            this._minimumDaysToRetain = minimumDaysToRetain;
            if (minimumDaysToRetain != null) {
                this.QueryParams.Add("minimum_days_to_retain", minimumDaysToRetain.ToString());
            }
            else
            {
                this.QueryParams.Remove("minimum_days_to_retain");
            }
            return this;
        }
        public ModifyDataPersistenceRuleSpectraS3Request WithType(DataPersistenceRuleType? type)
        {
            this._type = type;
            if (type != null) {
                this.QueryParams.Add("type", type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        
        public ModifyDataPersistenceRuleSpectraS3Request(string dataPersistenceRule)
        {
            this.DataPersistenceRule = dataPersistenceRule;
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/data_persistence_rule/" + DataPersistenceRule;
            }
        }
    }
}