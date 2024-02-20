angular
    .module('vcx').factory('AuthenticationService',
    ['Base64', '$http', '$cookieStore', '$rootScope', '$timeout', 'SocketService', 'apiManage', '$cookies',
    function (Base64, $http, $cookieStore, $rootScope, $timeout, SocketService, apiManage, $cookies) {
        var service = {};

            service.Login = function (companycode, password, module_name,callback) {




                /* Dummy authentication for testing, uses $timeout to simulate api call
                 ----------------------------------------------*/
                $timeout(function () {
                    var api_request_url = apiManage.apiList['login'].api;
                    var ls_params = {
                        user_code: companycode,
                        password: password,
                        organization_code: "rr",
                        module: module_name,
                        hostname: "ss",
                        captcharesponse:"45"

                    };

                    SocketService.post(api_request_url, ls_params).then(function (response) {
                        console.log(response.data);
                        callback(response.data);
                    });


                }, 1000);

            };
         

            service.SetCredentials = function (data) {
                $rootScope.globals = {
                    currentUser: {
                        user_code: data.user_code,
                        user_gid:data.user_gid,
                        UserName: data.user_name,
                        authdata: data.token,
                        profile_id: data.profile_id,
                        AdobeLinked: data.adobe_linked,
                        refresh_token:data.refresh_token,
                        usertransaction_gid: null

                    }
                };

                $http.defaults.headers.common['Authorization'] = data.token; // jshint ignore:line
                $cookies.putObject('globals', $rootScope.globals);
            };

            service.ClearCredentials = function () {
                $rootScope.globals = {
                    currentUser: {}
                };
              $cookies.remove('globals');
                $http.defaults.headers.common['Authorization'] = '';
            };
       
         return service;
       }])
    
angular
    .module('vcx').factory('Base64', function () {
            /* jshint ignore:start */

            var keyStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

            return {
                encode: function (input) {
                    var output = "";
                    var chr1, chr2, chr3 = "";
                    var enc1, enc2, enc3, enc4 = "";
                    var i = 0;

                    do {
                        chr1 = input.charCodeAt(i++);
                        chr2 = input.charCodeAt(i++);
                        chr3 = input.charCodeAt(i++);

                        enc1 = chr1 >> 2;
                        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                        enc4 = chr3 & 63;

                        if (isNaN(chr2)) {
                            enc3 = enc4 = 64;
                        } else if (isNaN(chr3)) {
                            enc4 = 64;
                        }

                        output = output +
                            keyStr.charAt(enc1) +
                            keyStr.charAt(enc2) +
                            keyStr.charAt(enc3) +
                            keyStr.charAt(enc4);
                        chr1 = chr2 = chr3 = "";
                        enc1 = enc2 = enc3 = enc4 = "";
                    } while (i < input.length);

                    return output;
                },

                decode: function (input) {
                    var output = "";
                    var chr1, chr2, chr3 = "";
                    var enc1, enc2, enc3, enc4 = "";
                    var i = 0;

                    // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
                    var base64test = /[^A-Za-z0-9\+\/\=]/g;
                    if (base64test.exec(input)) {
                        window.alert("There were invalid base64 characters in the input text.\n" +
                            "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                            "Expect errors in decoding.");
                    }
                    input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

                    do {
                        enc1 = keyStr.indexOf(input.charAt(i++));
                        enc2 = keyStr.indexOf(input.charAt(i++));
                        enc3 = keyStr.indexOf(input.charAt(i++));
                        enc4 = keyStr.indexOf(input.charAt(i++));

                        chr1 = (enc1 << 2) | (enc2 >> 4);
                        chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                        chr3 = ((enc3 & 3) << 6) | enc4;

                        output = output + String.fromCharCode(chr1);

                        if (enc3 != 64) {
                            output = output + String.fromCharCode(chr2);
                        }
                        if (enc4 != 64) {
                            output = output + String.fromCharCode(chr3);
                        }

                        chr1 = chr2 = chr3 = "";
                        enc1 = enc2 = enc3 = enc4 = "";

                    } while (i < input.length);

                    return output;
                },
                b64toBlob: function (b64Data, contentType, sliceSize) {
                    contentType = contentType || '';
                    sliceSize = sliceSize || 512;

                    var byteCharacters = atob(b64Data);
                    var byteArrays = [];

                    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
                        var slice = byteCharacters.slice(offset, offset + sliceSize);

                        var byteNumbers = new Array(slice.length);
                        for (var i = 0; i < slice.length; i++) {
                            byteNumbers[i] = slice.charCodeAt(i);
                        }

                        var byteArray = new Uint8Array(byteNumbers);

                        byteArrays.push(byteArray);
                    }

                    var blob = new Blob(byteArrays, { type: contentType });
                    return blob;
                }
            };

            /* jshint ignore:end */
        });