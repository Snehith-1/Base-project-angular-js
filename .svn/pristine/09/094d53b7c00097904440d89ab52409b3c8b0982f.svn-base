(function () {
    'use strict';

    angular
        .module('angle')
        .controller('topnavbarcontroller', topnavbarcontroller);

    topnavbarcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function topnavbarcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'topnavbarcontroller';

        activate();

        function activate() {
            $scope.user_name = localStorage.getItem('user_name');  
        }

        if (!$rootScope.$$listenerCount['downloadEvent']) {
            $rootScope.$on('downloadEvent', function (event, resp) {
                if (resp.data.status == true) {
                    if (resp.data.format == 'pdf') {
                        contentType = 'application/pdf';
                    }
                    else if (resp.data.format == 'xls') {
                        var contentType = 'application/vnd.ms-excel';
                    }
                    else if (resp.data.format == 'xlsx') {
                        var contentType = 'application/vnd.ms-excel';
                    }
                    var b64Data = resp.data.file;
                    var blob = b64toBlob(b64Data, contentType);
                    var blobUrl = URL.createObjectURL(blob);
                    var img = document.getElementById('btnpdf');
                    img.download = resp.data.name;
                    img.href = blobUrl;
                    img.click();
                } else {
                    Notify.alert('Error in downloading. please contact sysadmin',
                    {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
            });
        }

        if (!$rootScope.$$listenerCount['DocumentViewerListener']) {
            $rootScope.$on('DocumentViewerListener', function (event, resp) {
                if (resp.data.status == true) {
                    if (resp.data.format.toLowerCase() == 'pdf') {
                        contentType = 'application/pdf';
                    }  
                    else if (resp.data.format.toLowerCase() == 'png') {
                        var contentType = 'image/png';
                    }
                    else if (resp.data.format.toLowerCase() == 'jpg') {
                        var contentType = 'image/jpg';
                    }
                    else if (resp.data.format.toLowerCase() == 'jpeg') {
                        var contentType = 'image/jpeg';
                    }
                    else if (resp.data.format.toLowerCase() == 'txt') {
                        var contentType = 'text/plain';
                    }
                    else if (resp.data.format.toLowerCase() == 'html') {
                        var contentType = 'text/html';
                    }
                    var b64Data = resp.data.file;
                    var blob = b64toBlob(b64Data, contentType);
                    var blobUrl = URL.createObjectURL(blob);
                    const pdfWindow = window.open(""); 
                    pdfWindow.document.write("<iframe width='100%' height='100%'  src='" + blobUrl + "#toolbar=0" + "'></iframe>"); 
                } else {
                    Notify.alert('Error Occured. please contact sysadmin',
                    {
                        status: 'warning',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
            });
        } 

        function b64toBlob(b64Data, contentType, sliceSize) {
            contentType = contentType || '';
            sliceSize = sliceSize || 512; var byteCharacters = atob(b64Data);
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
            var blob = new Blob(byteArrays, { type: contentType }); return blob;
        }

        $scope.user_profile = function () {
            $location.url('app/MstUserProfile');
        }

    }
})();
