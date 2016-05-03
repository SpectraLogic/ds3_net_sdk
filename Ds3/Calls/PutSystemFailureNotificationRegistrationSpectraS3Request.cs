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
    public class PutSystemFailureNotificationRegistrationSpectraS3Request : Ds3Request
    {
        
        public string NotificationEndPoint { get; private set; }

        
        private HttpResponseFormatType _format;
        public HttpResponseFormatType Format
        {
            get { return _format; }
            set { WithFormat(value); }
        }

        public PutSystemFailureNotificationRegistrationSpectraS3Request WithFormat(HttpResponseFormatType format)
        {
            this._format = format;
            if (format != null) {
                this.QueryParams.Add("format", Format.ToString());
            }
            else
            {
                this.QueryParams.Remove("format");
            }
            return this;
        }

        private NamingConventionType _namingConvention;
        public NamingConventionType NamingConvention
        {
            get { return _namingConvention; }
            set { WithNamingConvention(value); }
        }

        public PutSystemFailureNotificationRegistrationSpectraS3Request WithNamingConvention(NamingConventionType namingConvention)
        {
            this._namingConvention = namingConvention;
            if (namingConvention != null) {
                this.QueryParams.Add("naming_convention", NamingConvention.ToString());
            }
            else
            {
                this.QueryParams.Remove("naming_convention");
            }
            return this;
        }

        private RequestType _notificationHttpMethod;
        public RequestType NotificationHttpMethod
        {
            get { return _notificationHttpMethod; }
            set { WithNotificationHttpMethod(value); }
        }

        public PutSystemFailureNotificationRegistrationSpectraS3Request WithNotificationHttpMethod(RequestType notificationHttpMethod)
        {
            this._notificationHttpMethod = notificationHttpMethod;
            if (notificationHttpMethod != null) {
                this.QueryParams.Add("notification_http_method", NotificationHttpMethod.ToString());
            }
            else
            {
                this.QueryParams.Remove("notification_http_method");
            }
            return this;
        }

        public PutSystemFailureNotificationRegistrationSpectraS3Request(string notificationEndPoint) {
            this.NotificationEndPoint = notificationEndPoint;
            
            this.QueryParams.Add("notification_end_point", NotificationEndPoint);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/system_failure_notification_registration/";
            }
        }
    }
}