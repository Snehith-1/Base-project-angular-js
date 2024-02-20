(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprBureauUpdateIndividualController', AgrMstSuprBureauUpdateIndividualController);

    AgrMstSuprBureauUpdateIndividualController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprBureauUpdateIndividualController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprBureauUpdateIndividualController';
        $scope.contact_gid = $location.search().lscontact_gid;
        var application_gid = $location.search().lsapplication_gid;
        var tab = $location.search().lstab;
        var status = $location.search().lsstatus;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();
        function activate() {

            var url = 'api/AgrMstSuprApplicationAdd/GetIndividualBureauTempClear';
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
                contact_gid: $scope.contact_gid
            };
            var url = 'api/AgrMstSuprApplicationAdd/GetContactBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.contactbureau_list;
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
            contact_gid: $scope.contact_gid
        };

        $scope.generateHighmarkReport = function () {
            $scope.html_content = '';
            var url = 'api/AgrBureauAPI/GetHighmarkCreditInfo';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.bureauscore_disabled = true;
                    $scope.bureauresponse_disabled = true;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureau_response = resp.data.bureau_response;                 
                    $scope.reportgeneration_success = true;
                    $scope.reportgeneration_message = 'Report Generated';
                    $scope.highmarkselected = false;
                    var param = {
                        contact2bureau_gid: $scope.contact2bureau_gid
                    };
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                    });
                    unlockUI();
                }
                else {
                    $scope.bureauscore_disabled = false;
                    $scope.bureauresponse_disabled = false;

                    $scope.reportgeneration_failure = true;
                    $scope.reportgeneration_message = 'Report Generation Failed';
                    $scope.highmarkselected = false;
                    unlockUI();
                }
            });
        };

        $scope.report_View = function (tmpcicdocument_gid) {
            var tmpcicdocument_gid = tmpcicdocument_gid;
            localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkReport";
            window.open(URL, '_blank');
        };
       


        $scope.CICDocumentUpload = function (val, val1, name) {
            //var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
            
            //            if (IsValidExtension == false) {
            //                Notify.alert("File format is not supported..!", {
            //                    status: 'danger',
            //                    pos: 'top-center',
            //                    timeout: 3000
            //                });
            //                return false;
            //            }

            //var item = {
            //    file: val[0]
            //};
            //var frm = new FormData();
            //frm.append('file', item.file);
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

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
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/AgrMstSuprApplicationAdd/CICIndividualDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        contact2bureau_gid: $scope.contact2bureau_gid
                    };
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
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
            var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocDelete';
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
                    contact2bureau_gid: $scope.contact2bureau_gid
                };
                var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.addbureau_individual = function () {
            if (($scope.cboBureauName == undefined) || ($scope.txtbureauscore_date == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            //else if ($scope.cicuploaddoc_list == null) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            else {
                var params = {
                    contact_gid: $scope.contact_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostCICUploadIndividual';
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
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        contact_gid: $scope.contact_gid
                    };
                    var url = 'api/AgrMstSuprApplicationAdd/GetContactBureauList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.contactbureau_list = resp.data.contactbureau_list;

                    });

                    $scope.cboBureauName = '';
                    $scope.txtbureauscore_date = '';
                    $scope.txtobservations = '';
                    $scope.txtbureau_response = '';
                    $scope.txtbureau_score = '';
                    $scope.cicuploaddoc_list = '';

                });
            }
        }

        $scope.bureau_delete = function (contact2bureau_gid) {
            var params = {
                contact2bureau_gid: contact2bureau_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteContactBureau';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }

            });
        }

        $scope.bureau_edit = function (contact2bureau_gid) {
            $location.url('app/AgrMstSuprApplcreationCICUploadEdit?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + $scope.contact_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.bureau_view = function (contact2bureau_gid) {
            $location.url('app/AgrMstSuprApplcreationCICUploadView?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + $scope.contact_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.CICUploads_Back = function () {
            if (tab == 'edit') {
                $location.url('app/AgrMstSuprApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
            }
            else if (tab == 'add') {
                $location.url('app/AgrMstSuprApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
            }
            else {
                $location.url('app/AgrMstSuprApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
            }
        }

        $scope.update_CICUploads = function () {
            var bureauname_name = $('#BureauName :selected').text();
            var params = {
                bureauname_name: bureauname_name,
                bureauname_gid: $scope.bureau_gid,
                bureau_score: $scope.txtbureau_score,
                bureauscore_date: $scope.txtscore_on,
                bureauscoredate: $scope.txtscore_on,
                observations: $scope.txtobservations,
                bureau_response: $scope.txtbureau_response,
                contact_gid: $scope.contact_gid

            }
            var url = 'api/AgrMstSuprApplicationAdd/PostCICUploadIndividual';
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
                $state.go('app.AgrMstSuprApplicationGeneralEdit');
            });
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }


    }
})();

