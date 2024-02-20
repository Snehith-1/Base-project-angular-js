(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentCheckListController', documentCheckListController);

    documentCheckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function documentCheckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentCheckListController';

        activate();

        function activate() {
            $scope.photouploaded = false;
            $scope.upload = true;
            $scope.IsVisible = false;
            $scope.rskdocumentlist = false;
            $scope.customer2sanction_gid = localStorage.getItem('customer2sanction_gid');
            var params =
                {
                    customer2sanction_gid: $scope.customer2sanction_gid
                }
            var url = "api/sanction/GetIdasSanctionDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.IsVisible = true;
                    $scope.idassanctiondocumentlist = resp.data.idassanctiondocument;
                }
            });

            var url = "api/sanction/GetRskSanctionDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rsksanctiondocumentlist = resp.data.rsksanctiondocument;
            });

            var url = "api/documentation/getrskdocumentationdtlList";

            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentationlist = resp.data.documentationdtl;
            });

            var url = 'api/sanction/GetSanctionDtls';
            var params = {
                sanction_gid: $scope.customer2sanction_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanction_refno;
                $scope.SanctionDate = resp.data.sanction_date;
                $scope.SanctionAmount = resp.data.sanction_amount;
                $scope.FacilityType = resp.data.facility_type;
                $scope.customerName = resp.data.customername;
                $scope.Customerurn = resp.data.customer_urn;
                $scope.collateral_security = resp.data.collateral_security;
                $scope.zonalHeadName = resp.data.zonal_name;
                $scope.businessHeadName = resp.data.businesshead_name;
                $scope.clusterManager = resp.data.cluster_manager_name;
                $scope.creditManager = resp.data.creditmanager_name;
                $scope.relationshipmgmt = resp.data.relationshipmgmt_name;
                $scope.customercode = resp.data.customercode;
                $scope.verticalCode = resp.data.vertical_code;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.riskmanager = resp.data.riskmanager;
            });
        }

        $scope.back = function () {
            $state.go('app.sanctionManagement')
        }
        $scope.uploadidasdocument_sub = function (val, val1, name, document_gid) {
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

            // var item = {
            //     name: val[0].name,
            //     file: val[0]
            // };
            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_gid', document_gid);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadidasSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        $scope.uploadidasdocument = function (val, val1, name, sanctiondocument_gid) {
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

            // var item = {
            //     name: val[0].name,
            //     file: val[0]
            // };
            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('sanctiondocument_gid', localStorage.getItem('sanctiondocument_gid'));
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.file_name = resp.data.file_name;
                    $scope.photouploaded = true;
                    $scope.upload = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        $scope.uploaddocument = function (val, val1, name) {
            var frm = new FormData(); //docchecklist

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

            // var item = {
            //     name: val[0].name,
            //     file: val[0]
            // };
            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('customer2sanction_gid', localStorage.getItem('customer2sanction_gid'));
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.Sanctiondocupload_list = resp.data.Sanctiondoc_upload;
                    $scope.photouploaded = true;
                    $scope.upload = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }



        $scope.documentadd = function () {
            if ($scope.txtremarks == "" || $scope.txtremarks == undefined) {
                Notify.alert('Enter Remarks..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                return;
            }
            else {
                if ($scope.Sanctiondocupload_list.length > 0) {
                    lockUI();
                    $scope.customer2sanction_gid = localStorage.getItem('customer2sanction_gid');
                    var params = {
                        customer2document_gid: $scope.cbodocumentationname.customer2document_gid,
                        customer2sanction_gid: $scope.customer2sanction_gid,
                        customer_gid: $scope.customer_gid,
                        document_remarks: $scope.txtremarks,
                    }
                    var url = "api/sanction/postsanctiondocument";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            // $scope.Sanctiondoc_upload_list = resp.data.Sanctiondoc_upload;
                            $scope.txtremarks = "";
                            $("#addrskupload").val('');
                            $scope.test_document = "";
                            $scope.file_name = undefined;
                            $scope.cbodocumentationname = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
                else {
                    Notify.alert('Kindly Upload Document..!', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return;
 
                }

            }


        }


        $scope.docdelete = function (document_gid) {
            var params = {
                document_gid: document_gid
            };

            lockUI();
            var url = "api/sanction/UploadDocDelete";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            unlockUI();
        }



        $scope.downloads = function (val1, val2) { 
            DownloaddocumentService.Downloaddocument(val1, val2); 
        }




    }

})();
