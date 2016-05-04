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
    public class GetTapePartitionsWithFullDetailsSpectraS3Request : Ds3Request
    {
        
        
        private ImportExportConfiguration? _importExportConfiguration;
        public ImportExportConfiguration? ImportExportConfiguration
        {
            get { return _importExportConfiguration; }
            set { WithImportExportConfiguration(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private string _libraryId;
        public string LibraryId
        {
            get { return _libraryId; }
            set { WithLibraryId(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private int? _pageLength;
        public int? PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        private int? _pageOffset;
        public int? PageOffset
        {
            get { return _pageOffset; }
            set { WithPageOffset(value); }
        }

        private string _pageStartMarker;
        public string PageStartMarker
        {
            get { return _pageStartMarker; }
            set { WithPageStartMarker(value); }
        }

        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { WithSerialNumber(value); }
        }

        private TapePartitionState? _state;
        public TapePartitionState? State
        {
            get { return _state; }
            set { WithState(value); }
        }

        public GetTapePartitionsWithFullDetailsSpectraS3Request WithImportExportConfiguration(ImportExportConfiguration? importExportConfiguration)
        {
            this._importExportConfiguration = importExportConfiguration;
            if (importExportConfiguration != null) {
                this.QueryParams.Add("import_export_configuration", importExportConfiguration.ToString());
            }
            else
            {
                this.QueryParams.Remove("import_export_configuration");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithLastPage(bool? lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null) {
                this.QueryParams.Add("last_page", lastPage.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_page");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithLibraryId(Guid? libraryId)
        {
            this._libraryId = libraryId.ToString();
            if (libraryId != null) {
                this.QueryParams.Add("library_id", libraryId.ToString());
            }
            else
            {
                this.QueryParams.Remove("library_id");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithLibraryId(string libraryId)
        {
            this._libraryId = libraryId;
            if (libraryId != null) {
                this.QueryParams.Add("library_id", libraryId);
            }
            else
            {
                this.QueryParams.Remove("library_id");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null) {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithPageLength(int? pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null) {
                this.QueryParams.Add("page_length", pageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithPageOffset(int? pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null) {
                this.QueryParams.Add("page_offset", pageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker.ToString();
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", pageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithPageStartMarker(string pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", pageStartMarker);
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithQuiesced(Quiesced? quiesced)
        {
            this._quiesced = quiesced;
            if (quiesced != null) {
                this.QueryParams.Add("quiesced", quiesced.ToString());
            }
            else
            {
                this.QueryParams.Remove("quiesced");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithSerialNumber(string serialNumber)
        {
            this._serialNumber = serialNumber;
            if (serialNumber != null) {
                this.QueryParams.Add("serial_number", serialNumber);
            }
            else
            {
                this.QueryParams.Remove("serial_number");
            }
            return this;
        }
        public GetTapePartitionsWithFullDetailsSpectraS3Request WithState(TapePartitionState? state)
        {
            this._state = state;
            if (state != null) {
                this.QueryParams.Add("state", state.ToString());
            }
            else
            {
                this.QueryParams.Remove("state");
            }
            return this;
        }

        
        public GetTapePartitionsWithFullDetailsSpectraS3Request() {
            
            this.QueryParams.Add("full_details", null);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/tape_partition/";
            }
        }
    }
}