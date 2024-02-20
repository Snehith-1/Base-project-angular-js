'use strict'
angular
    .module('angle')
.factory('DownloaddocumentService', ['SocketService', '$rootScope', 'apiManage', '$state', '$cookieStore', 'ngDialog',
    function (SocketService, $rootScope, apiManage, $state, $cookieStore, ngDialog) {
        var csfactory = {};
        csfactory.Downloaddocument = function (val1, val2) { 
            var params = {
                file_path: val1,
                file_name: val2
            }
            var url = 'api/azurestorage/DownloadDocument'; 
            lockUI(); 
            SocketService.post(url, params).then(function (resp) { 
                unlockUI();
                if (resp.data.status == true)
                     $rootScope.$emit('downloadEvent', resp);
                else {
                    return resp;
                } 
            });
        } 

        csfactory.OtherDownloaddocument = function (val1, val2, other_download) {
            var params = {
                file_path: val1,
                file_name: val2,
                other_download: other_download
            }
            var url = 'api/azurestorage/OtherDownloadDocument';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true)
                    $rootScope.$emit('downloadEvent', resp);
                else {
                    return resp;
                }
            });
        }
        
        // Document Viewer
        
        csfactory.DocumentViewer = function (val1, val2) { 
            var params = {
                file_path: val1,
                file_name: val2
            }
            var url = 'api/azurestorage/DownloadDocument'; 
            lockUI(); 
            SocketService.post(url, params).then(function (resp) { 
                unlockUI();
                if (resp.data.status == true)
                     $rootScope.$emit('DocumentViewerListener', resp);
                else {
                    return resp;
                } 
            });
        }
     
        csfactory.OtherDocumentViewer = function (val1, val2, other_download) {
            var params = {
                file_path: val1,
                file_name: val2,
                other_download: other_download
            }
            var url = 'api/azurestorage/OtherDownloadDocument';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true)
                    $rootScope.$emit('DocumentViewerListener', resp);
                else {
                    return resp;
                }
            });
        }

        return csfactory;

    }]);
