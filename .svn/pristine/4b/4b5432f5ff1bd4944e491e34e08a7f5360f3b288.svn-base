(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BureauUpdateInstitutionController', BureauUpdateInstitutionController);

    BureauUpdateInstitutionController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function BureauUpdateInstitutionController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'BureauUpdateInstitutionController';
        $scope.institution_gid = $location.search().lsinstitution_gid;
        var application_gid = $location.search().lsapplication_gid;
        var tab = $location.search().lstab;
        var status = $location.search().lsstatus;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();
        function activate() {

            var url = 'api/MstApplicationAdd/GetInstitutionBureauTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.txtbureauscore_date = new Date().toLocaleDateString('en-GB').replaceAll('/','-');

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
                institution_gid: $scope.institution_gid
            };
            var url = 'api/MstApplicationAdd/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
            });

        }

        $scope.bureauname_change = function () {
            var bureauname_name = $('#BureauName :selected').text();
          if (bureauname_name == 'High Mark') {
                $scope.highmarkselected = true;
            }
            else if(bureauname_name == 'TransUnion'){
                $scope.transUnionSelected = true;
            } 
            else {
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

                    $scope.highmarkselected = false;
                    unlockUI();
                }
            });
        };

        $scope.report_View = function (tmpcicdocument_gid) {
            var tmpcicdocument_gid = tmpcicdocument_gid;
            localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkInstitutionReport";
            window.open(URL, '_blank');
        };

      

         $scope.CICUploads_Back = function () {
             $state.go('app.MstApplicationGeneralEdit');
         } 



        $scope.CICDocumentUpload = function (val, val1, name) {
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

                    return false;
                }
            }



            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstApplicationAdd/CICInstitutionDocumentUpload';
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

        //$scope.downloads = function (val1, val2) {
        //    DownloaddocumentService.Downloaddocument(val1, val2);
        //}

        $scope.downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.addbureau_institution = function () {
            if (($scope.cboBureauName == undefined) || ($scope.txtbureauscore_date == undefined) || ($scope.txtbureau_score == undefined) 
            || ($scope.txtbureau_response == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if($scope.txtbureauscore_date == new Date().toLocaleDateString('en-GB').replaceAll('/','-')) {
                    $scope.txtbureauscore_date = new Date();
                }
                var params = {                    
                    institution_gid: $scope.institution_gid,                   
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,                    
                }
                var url = 'api/MstApplicationAdd/PostCICUploadInstitution';
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
                        institution_gid: $scope.institution_gid
                    };
                    var url = 'api/MstApplicationAdd/GetInstitutionBureauList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.institutionbureau_list = resp.data.institutionbureau_list;

                    });

                    $scope.cboBureauName = '';
                    $scope.txtbureauscore_date = new Date().toLocaleDateString('en-GB').replaceAll('/','-');
                    $scope.txtobservations = '';
                    $scope.txtbureau_response = '';
                    $scope.txtbureau_score = '';
                    $scope.cicuploaddoc_list = '';               

                });
            }
        }

        $scope.bureau_delete = function (institution2bureau_gid) {
            var params = {
                institution2bureau_gid: institution2bureau_gid
            }
            var url = 'api/MstApplicationAdd/DeleteInstitutionBureau';
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

        //$scope.bureau_edit = function (institution2bureau_gid) {
        //    $location.url('app/MstApplcreationCICUploadInstEdit?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid);
        //}

        //$scope.bureau_view = function (institution2bureau_gid) {
        //    $location.url('app/MstApplcreationCICUploadInstView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid);
        //}

        $scope.bureau_edit = function (institution2bureau_gid) {
            $location.url('app/MstApplcreationCICUploadInstEdit?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.bureau_view = function (institution2bureau_gid) {
            $location.url('app/MstApplcreationCICUploadInstView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.CICUploads_Back = function () {
            if (tab == 'edit') {
                $location.url('app/MstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
            }
            else if (tab == 'add') {
                $location.url('app/MstApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
            }
            else {
                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=' + tab + '&lsstatus=' + status);
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
            var url = 'api/MstApplicationAdd/PostCICUploadIndividual';
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
                $state.go('app.MstApplicationGeneralEdit');
            });
        }

        //$scope.downloadall = function () {
        //    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
        //        DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
        //    }
        //}

        $scope.downloadall = function () {

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

