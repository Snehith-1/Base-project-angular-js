(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplcreationCICUploadInstEditController', AgrMstSuprApplcreationCICUploadInstEditController);

    AgrMstSuprApplcreationCICUploadInstEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$filter', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprApplcreationCICUploadInstEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $filter, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplcreationCICUploadInstEditController';
        $scope.institution2bureau_gid = $location.search().lsinstitution2bureau_gid;
        $scope.institution_gid = $location.search().lsinstitution_gid;
        var application_gid = $location.search().lsapplication_gid;
        var tab = $location.search().lstab;
        var status = $location.search().lsstatus;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;
        var lstab = $location.search().lstab;

        activate();
        function activate() {
            var url = 'api/AgrMstSuprApplicationAdd/GetInstitutionBureauTempClear';
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

          
            var url = 'api/AgrMstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var param = {
                institution2bureau_gid: $scope.institution2bureau_gid
            };

            var url = 'api/AgrMstSuprApplicationEdit/CICInstitutionEdit';

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

            var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });

        }

        $scope.bureauname_change = function () {
            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'High Mark') {
                $scope.highmarkselected = true;
            } else {
                $scope.highmarkselected = false;
            }
        };

        var params = {
            institution_gid: $scope.institution_gid
        };

        $scope.generateHighmarkDetails = function () {
            $scope.html_content = '';
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

                    $scope.highmarkselected = false;
                    var param = {
                        institution2bureau_gid: $scope.institution2bureau_gid
                    };
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
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

                    $scope.highmarkselected = false;
                    unlockUI();
                }
            });
        };

        $scope.report_View = function (tmpcicdocument_gid) {
            var tmpcicdocument_gid = tmpcicdocument_gid;
            localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrHighmarkInstitutionReport";
            window.open(URL, '_blank');
        };
 
        $scope.CICUploads_Back = function () {
            $location.url('app/AgrMstSuprBureauUpdateInstitution?lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.CICDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                
                var frm = new FormData();                
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    
                    frm.append(fi.files[i].name, fi.files[i])
                  
                    $scope.uploadfrm = frm;
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
                frm.append('project_flag', "documentformatonly");
                var url = 'api/AgrMstSuprApplicationAdd/CICInstitutionDocumentUpload';
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
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
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

        $scope.uploaddocumentcancel = function (tmpcicdocument_gid) {
            lockUI();
            var params = {
                tmpcicdocument_gid: tmpcicdocument_gid
            }
            console.log(params);
            var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocDelete';
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
                var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }



        $scope.update_CICUploads = function () {
            //if ($scope.cicuploaddoc_list == null) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}

            try {
                if ($scope.txtbureauscore_date.split("-"))
                    $scope.txtbureauscore_date = $scope.txtbureauscore_date.split("-").reverse().join("-");
            }
            catch (e) {

                $scope.txtbureauscore_date = $scope.txtbureauscore_date

            }

            var bureauname_name = $('#BureauName :selected').text();
            var params = {
                bureauname_name: bureauname_name,
                bureauname_gid: $scope.bureau_gid,
                bureau_score: $scope.txtbureau_score,
                bureauscore_date: $scope.txtbureauscore_date,         
                observations: $scope.txtobservations,
                bureau_response: $scope.txtbureau_response,
                institution_gid: $scope.institution_gid,
                institution2bureau_gid: $scope.institution2bureau_gid

            }
            var url = 'api/AgrMstSuprApplicationAdd/UpdateCICUploadInstitution';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/AgrMstSuprBureauUpdateInstitution?lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                //$location.url('app/AgrMstSuprBureauUpdateInstitution?lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
            });
        }  
        
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }

    }
})();

