
'use strict'
angular
    .module('vcx')
.factory('CommonService', ['SocketService', 'apiManage', '$state', '$cookieStore', 'ngDialog',
    function (SocketService, apiManage, $state, $cookieStore, ngDialog) {
        var csfactory = {};
        csfactory.PreviewPopUp = function (url) {
            //if (is_transcript == "Y") {
            //   
            //}
            //else {
            //    
            //}
            ngDialog.open({
                //templateUrl: "app/views/preview.html",
                //size: 'md',
                //backdrop: 'static',
                //keyboard: false,
                closeByDocument: false,
                plain: false,
                template: 'app/views/preview.html',
                controller: ['$scope', 'ScopeValueService', function ($scope, ScopeValueService) {
                    function pop() {                    
                       // alert(ScopeValueService.get('MyCertifiedDocumentController').is_transcript == "Y");
                        if (ScopeValueService.get('MyCertifiedDocumentController').is_transcript == "Y") {
                            $scope.note = "Note: Please collect the hard copy of the transcript from the University.";
                        }
                        else if (ScopeValueService.get('MyCertifiedDocumentController').preview_content != "" && ScopeValueService.get('MyCertifiedDocumentController').status == "Approved") {
                            $scope.note = ScopeValueService.get('MyCertifiedDocumentController').preview_content;
                           }

                        else {
                            $scope.note = "";
                        }
                        $scope.pdf_file_url = url;
                    }
                    pop();

                }],
                closeButton: true,
                //resolve: {
                //    pdf_file_url: function () {
                //        return scope;
                //    }
                //}
            });
        }
        return csfactory;

    }]);