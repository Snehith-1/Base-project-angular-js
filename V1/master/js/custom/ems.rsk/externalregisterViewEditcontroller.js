(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalregisterViewEdit', externalregisterViewEdit);

    externalregisterViewEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function externalregisterViewEdit($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalregisterViewEdit';

        activate();

        function activate() {
            lockUI();
            var params = {
                externalregister_gid: localStorage.getItem('externalregister_gid')
            }
            var url = "api/externalVendor/getexternalRegisterdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtexternal_vendorcode = resp.data.external_vendorcode;
                $scope.txtexternal_vendorName = resp.data.external_vendorname;
                $scope.txtcontact_person = resp.data.contact_person;
                $scope.txtemail_ID = resp.data.contact_emailid;
                $scope.txtcontact_Number = resp.data.contact_number;
                $scope.txtaddress_line1 = resp.data.address_line1;
                $scope.txtaddress_line2 = resp.data.address_line2;
                $scope.state_name = resp.data.state_name;
                $scope.cbostategid = resp.data.state_gid;
                $scope.cbodistrict_gid = resp.data.district_gid;
                $scope.district_name = resp.data.district_name;
                $scope.txtcountry = resp.data.country_name;
                $scope.txtpostalCode = resp.data.postal_code;
                
                if (resp.data.photo_path != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.photo_path;
                    var str = str.split("StoryboardAPI");
                    var relpath1 = str[1].replace("\\", "/");
                    $scope.photo_path = url.concat(relpath1); 
                }
                else {
                    $scope.photo_path = resp.data.photo_path;
                }
                var state_gid = {
                    state_gid: resp.data.state_gid
                }
                var url = "api/rmMapping/getdistrictdtls";
                SocketService.getparams(url, state_gid).then(function (resp) {
                    $scope.districtdtl = resp.data.statedtl;
                });

            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;
            });
            unlockUI();
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

        $scope.registrationUpdate = function () {
            var params = {
                external_vendorcode: $scope.txtexternal_vendorcode,
                external_vendorname: $scope.txtexternal_vendorName,
                contact_person: $scope.txtcontact_person,
                contact_emailid: $scope.txtemail_ID,
                contact_number: $scope.txtcontact_Number,
                address_line1: $scope.txtaddress_line1,
                address_line2: $scope.txtaddress_line2,
                state_gid: $scope.cbostategid,
                district_gid: $scope.cbodistrict_gid,
                country_name: $scope.txtcountry,
                postal_code: $scope.txtpostalCode,
                externalregister_gid: localStorage.getItem('externalregister_gid')
            }

            var url = "api/externalVendor/updateexternalRegistration";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

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
