﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditInstitutionBureauEditController', MstCreditInstitutionBureauEditController);

    MstCreditInstitutionBureauEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditInstitutionBureauEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditInstitutionBureauEditController';
        $scope.institution2bureau_gid = $location.search().lsinstitution2bureau_gid;
        $scope.institution_gid = $location.search().lsinstitution_gid;
        $scope.application_gid = $location.search().lsapplication_gid;
        $scope.lspage = $location.search().lspage;

        var lstab = $location.search().lstab;

        activate();
        function activate() {

            var url = 'api/MstApplicationAdd/GetInstitutionBureauTempClear';
            SocketService.get(url).then(function (resp) {
            });


            // Calender Popup... //
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];


            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var param = {
                institution2bureau_gid: $scope.institution2bureau_gid
            };

            var url = 'api/MstApplicationEdit/CICInstitutionEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bureauname_name = resp.data.bureauname_name;
                $scope.bureau_gid = resp.data.bureauname_gid;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtbureauscore_date = resp.data.bureauscore_date;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureau_response;
                $scope.institution2bureau_gid = resp.data.institution2bureau_gid;

                unlockUI();
            });
            var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

        $scope.bureauname_change = function () {
            var lspage = $scope.lspage;
            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'High Mark') {
                $scope.reportbureauselected = true;
            } else if(bureauname_name == 'TransUnion' && lspage != 'PendingCADReview' && lspage !='CADAcceptanceCustomers'){
                $scope.reportbureauselected = true;
            }else {
                $scope.reportbureauselected = false;
            }
        };

        $scope.generateDetails = function () {
            $scope.html_content = '';

            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'TransUnion') {
                
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/BureauAPI/GetTransUnionCommercialCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;                       
                        $scope.txtbureau_score = resp.data.bureau_score;                       
                        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;

                        var param = {
                            institution2bureau_gid: $scope.institution2bureau_gid
                        }
                        var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });

                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;                       

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });
                


            }
            else if (bureauname_name == 'High Mark') {
                $scope.html_content = '';
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/BureauAPI/GetHighmarkInstitutionCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;
                        $scope.bureauresponse_disabled = true;
                        $scope.txtbureau_score = resp.data.bureau_score;
                        $scope.txtbureau_response = resp.data.bureau_response;

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.reportbureauselected = false;
                        var param = {
                            institution2bureau_gid: $scope.institution2bureau_gid
                        };
                        var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });
                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;
                        $scope.bureauresponse_disabled = false;

                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });
            }


        };

        $scope.report_View = function (tmpcicdocument_gid,document_name) {
            if(document_name.includes('TransUnion')) {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/TransUnionInstitutionReport";
                window.open(URL, '_blank');
               } else {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkInstitutionReport";
                window.open(URL, '_blank');
               }
            // var tmpcicdocument_gid = tmpcicdocument_gid;
            // localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            // var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkInstitutionReport";
            // window.open(URL, '_blank');
        };

        $scope.CICUploads_Back = function () {
            $location.url('app/MstCreditInstitutionDtlAdd?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage);
        }

        $scope.CICDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {

                var frm = new FormData();
                
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                    
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationAdd/CICInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        institution2bureau_gid: $scope.institution2bureau_gid
                    };
                    var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.documentviewer = function (val1, val2, val3) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
            }

        }

        $scope.uploaddocumentcancel = function (tmpcicdocument_gid) {
            lockUI();
            var params = {
                tmpcicdocument_gid: tmpcicdocument_gid
            }
            var url = 'api/MstApplicationEdit/CICUploadInstitutionDocDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    institution2bureau_gid: $scope.institution2bureau_gid
                };
                var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.update_CICUploads = function () {
            var bureauname_name = $('#BureauName :selected').text();
            var params = {
                bureauname_name: bureauname_name,
                bureauname_gid: $scope.bureau_gid,
                bureau_score: $scope.txtbureau_score,
                bureauscoredate: $scope.txtbureauscore_date,
                observations: $scope.txtobservations,
                bureau_response: $scope.txtbureau_response,
                institution_gid: $scope.institution_gid,
                institution2bureau_gid: $scope.institution2bureau_gid

            }
            var url = 'api/MstApplicationAdd/UpdateCICUploadInstitution';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/MstCreditInstitutionDtlAdd?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage);
            });
        }       

        $scope.downloadallcic = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                if ($scope.cicuploaddoc_list[i].migration_flag == 'N') {
                    DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name, $scope.cicuploaddoc_list[i].migration_flag);
                }
            }
        }

    }
})();

