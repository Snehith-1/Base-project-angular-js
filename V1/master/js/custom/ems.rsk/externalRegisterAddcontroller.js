(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalRegisterAddcontroller', externalRegisterAddcontroller);

    externalRegisterAddcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function externalRegisterAddcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalRegisterAddcontroller';

        activate();

        function activate() {
            $scope.photouploaded = false;
            $scope.upload = true;
            var url = "api/externalVendor/tmpexternalphotoclear";
            SocketService.get(url).then(function (resp) {
                
            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;

            });
        }

        $scope.onchangestate = function (cbostategid) {
            lockUI();
            var params = {
                state_gid: cbostategid
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.upload = function (val, val1, name) {

            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "photoformatonly");

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
            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         $("#addupload").val('');

            //         return false;

            //     }
            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('customer_gid', localStorage.getItem('customer_gid'))
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/externalVendor/ExternalphotoUpload";
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.Rskexternalvendordoc;
                $scope.photouploaded = true;
                $scope.upload = false;

                
            });
            $("#addupload").val('');
        }

        $scope.registrationsubmit = function () {
            lockUI();
            var params = {
                external_vendorcode: $scope.txtexternal_vendorCode,
                external_vendorname: $scope.txtexternal_vendorName,
                contact_person: $scope.txtcontact_person,
                contact_emailid: $scope.txtemail_ID,
                contact_number: $scope.txtcontact_Number,
                address_line1: $scope.txtaddress_line1,
                address_line2: $scope.txtaddress_line2,
                state_gid: $scope.cbostategid,
                district_gid: $scope.cbodistrictgid,
                country_name: $scope.txtcountry,
                postal_code: $scope.txtpostalCode
            }

            var url = "api/externalVendor/postexternalRegistration";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    //var url = "api/externalVendor/externalphoto";
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.externalRegister');
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

        $scope.cancel = function () {
            $state.go('app.externalRegister');
        }
    }
})();
