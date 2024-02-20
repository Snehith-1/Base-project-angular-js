﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstcustomeraddController', MstcustomeraddController);

    MstcustomeraddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','cmnfunctionService'];

    function MstcustomeraddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstcustomeraddController';
        activate();

        function activate() {
            var url = window.location.href;
            var relPath = url.split("lsredirect=");
            $scope.relpath1 = relPath[1];
            $scope.addIndividual = false;
            $scope.addinstitution = false;
            $scope.SA_yes = true;
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;
            $scope.clickadd = false;
            $scope.Warning = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/customer/cMmail';
            SocketService.get(url).then(function (resp) {
                $scope.txtccmail = resp.data.ccmail;
            });
            
            var url = 'api/MstCustomerAdd/GetTempClear';
            SocketService.get(url).then(function (resp) {

            });
            if ($scope.relpath1 == "add") {

           
                    var url = 'api/MstCustomerAdd/GetCustomer2UserDtl';
                    SocketService.get(url).then(function (resp) {
                        $scope.customer2user_list = resp.data.customer2userdtl_list;
                    });
               
            }
            else {
                var url = 'api/MstCustomerAdd/OverallTempClear';
                SocketService.get(url).then(function (resp) {

                });
            }
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/ValueChain/GetValueChainASC';
            SocketService.get(url).then(function (resp) {
                $scope.valuechain_list = resp.data.valuechain_list;
            });
            var url = 'api/UserType/GetUserTypeASC';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });
            var url = 'api/Designation/GetDesignationASC';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });
            var url = 'api/Constitution/GetconstitutionASC';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });
            var url = 'api/BusinessUnit/GetBusinessUnitASC';
            SocketService.get(url).then(function (resp) {
                $scope.businessunit_list = resp.data.businessunit_list;
            });

            var url = 'api/AddressType/GetAddressTypeASC';
            SocketService.get(url).then(function (resp) {
                $scope.addresstype_list = resp.data.addresstype_list;
            });
            var url = 'api/Designation/GetDesignationASC';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
                  });
            $scope.txtcountry = "India";
        }

        $scope.onselectedsa_yes = function () {
            if ($scope.rdbassociate == 'Yes') {
                var url = 'api/MstApplication360/GetAssociateMasterASC';
                SocketService.get(url).then(function (resp) {
                    $scope.associatemaster_list = resp.data.saassociatemaster_list;
                });
                $scope.SA_yes = true;
            }
            else {
                $scope.SA_yes = false;
                $scope.cbosa_idname = '';
                $scope.txtsa_payout = '';
            }
        }
        $scope.individual = function () {

            $scope.txtindividualname = '';
            $scope.txtdob = '';
            $scope.txtage = '';
            $scope.rdbgender = '';
            $scope.txtpersonalemail_address = '';
            $scope.txtofficialemail_address = '';
            $scope.txtindividualtelephoneno = '';
            $scope.txtcontactperson = '';
            $scope.txtindividualaadhar_no = '';
            $scope.txtindividualpan_no = '';
            $scope.cboindividualuser_type = '';
            $scope.addIndividual = true;
            $scope.addinstitution = false;
            $scope.clickadd = true;
            $scope.Warning = false;
            addresslist();
            idprooflist();
            mobileno_list();
        }
        $scope.institution = function () {

            $scope.txtinstitution_name = '';
            $scope.txtcoi_date = '';
            $scope.txtinstitutioncin_no = '';
            $scope.txtinstitutiontelephoneno = '';
            $scope.txtinstitutioncontact_name = '';
            $scope.txtinstitutioncontact_designation = '';
            $scope.txtcompanyemail_address = '';
            $scope.cbocompanytype = '';
            $scope.txtinstitutionlandmark = '';
            $scope.txtinstitution_year = '';
            $scope.txtinstitution_month = '';
            $scope.txtinstitution_creditrating = '';
            $scope.txtinstitution_escrow = '';
            $scope.txtinstitutionaadhar_no = '';
            $scope.txtinstitutionpan_no = '';
            $scope.txtinstitutiongst_no = '';
            $scope.cbouser_type = '';
            $scope.addIndividual = false;
            $scope.addinstitution = true;
            $scope.clickadd = true;
            $scope.Warning = false;
            addresslist();
            idprooflist();
            mobileno_list();
            member_list();
        }
        $scope.individualcancel = function () {
            $scope.txtindividualname = '';
            $scope.txtdob = '';
            $scope.txtage = '';
            $scope.rdbgender = '';
            $scope.txtpersonalemail_address = '';
            $scope.txtofficialemail_address = '';
            $scope.txtindividualtelephoneno = '';
            $scope.txtcontactperson = '';
            $scope.txtindividualaadhar_no = '';
            $scope.txtindividualpan_no = '';
            $scope.cboindividualuser_type = '';
            $scope.addIndividual = false;
            $scope.clickadd = false;
            $scope.Warning = false;
            var url = 'api/MstCustomerAdd/GetTempClear';
            SocketService.get(url).then(function (resp) {

            });
            addresslist();
            idprooflist();
            mobileno_list();
            member_list();
        }
        $scope.institutioncancel = function () {
            $scope.txtinstitution_name = '';
            $scope.txtcoi_date = '';
            $scope.txtinstitutioncin_no = '';
            $scope.txtinstitutiontelephoneno = '';
            $scope.txtinstitutioncontact_name = '';
            $scope.txtinstitutioncontact_designation = '';
            $scope.txtcompanyemail_address = '';
            $scope.cbocompanytype = '';
            $scope.txtinstitutionlandmark = '';
            $scope.txtinstitution_year = '';
            $scope.txtinstitution_month = '';
            $scope.txtinstitution_creditrating = '';
            $scope.txtinstitution_escrow = '';
            $scope.txtinstitutionaadhar_no = '';
            $scope.txtinstitutionpan_no = '';
            $scope.txtinstitutiongst_no = '';
            $scope.cbouser_type = '';
            $scope.addinstitution = false;
            $scope.clickadd = false;
            $scope.Warning = false;
            var url = 'api/MstCustomerAdd/GetTempClear';
            SocketService.get(url).then(function (resp) {

            });
            addresslist();
            idprooflist();
            mobileno_list();
            member_list();
        }
        $scope.ngchange = function () {
            $scope.Warning = false;
        }
        $scope.customerback = function (val) {
            $state.go('app.MstCustomerSummary');
        }
        //-----------Address Type Popup--------------//
        $scope.addaddress = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });
                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        address_type: $scope.cboaddresstype.address_type,
                        primary_address: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.cbostate.state_name,
                        state_gid: $scope.cbostate.state_gid,
                        district: $scope.txtdistrict
                    }
                    console.log(params);
                    var url = 'api/MstCustomerAdd/postaddresstype';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            addresslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }
        //--------Delete Address--------//
        $scope.deleteaddress = function (customer2address_gid) {
            var params =
                {
                    customer2address_gid: customer2address_gid
                }
            var url = 'api/MstCustomerAdd/DeleteAddress';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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

                addresslist();
            });

        }
        function addresslist() {
            var url = 'api/MstCustomerAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.address_list = resp.data.address_list;
                console.log(resp.data.address_list);
            });
        }
        //----------Submit - Identity Proof---------------//
        $scope.identityproof_add = function () {
            if (($scope.txtid_no == undefined) || ($scope.txtid_no == '') || ($scope.cboidentity_proof == undefined)) {
                Notify.alert('Enter ID Proof/ Select ID Type');
            }
            else {
                var params = {
                    idproof_type: $scope.cboidentity_proof,
                    idproof_no: $scope.txtid_no,
                }
                var url = 'api/MstCustomerAdd/postidproof';
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
                    $scope.txtid_no = '';
                    $scope.cboidentity_proof = null;
                    idprooflist();
                });
            }
        }
        //--------Delete ID Proof--------//
        $scope.deleteidproof = function (customer2identityproof_gid) {
            var params =
                {
                    customer2identityproof_gid: customer2identityproof_gid
                }
            console.log(params);
            var url = 'api/MstCustomerAdd/DeleteIDProof';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                $scope.cboidentity_proof = '';
                $scope.txtid_no = '';
                idprooflist();
            });

        }

        function idprooflist() {
            var url = 'api/MstCustomerAdd/GetidproofList';
            SocketService.get(url).then(function (resp) {
                $scope.idproof_list = resp.data.idproof_list;
            });
        }
        //----------Institution Submit - Mobile No---------------//
        $scope.institutionmobileno_add = function () {
            console.log($scope.txtmobile_no);
            console.log($scope.rdbprimarymobile_no);
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
            }
            else {


                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimarymobile_no
                }
                var url = 'api/MstCustomerAdd/postmobileno';
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
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no ==false;
                    mobileno_list();
                });
            }
        }
        //----------Individual Submit - Mobile No---------------//
        $scope.mobileno_add = function () {
            console.log($scope.rdbmobile_no);
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
            }
            else {


                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbmobile_no
                }
                var url = 'api/MstCustomerAdd/postmobileno';
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
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbmobile_no == false
                });
            }
        }
        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (customer2mobileno_gid) {
            var params =
                {
                    customer2mobileno_gid: customer2mobileno_gid
                }
            console.log(params)
            var url = 'api/MstCustomerAdd/DeleteMobileNo';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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

                mobileno_list();
            });

        }
        function mobileno_list() {
            var url = 'api/MstCustomerAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
              
            });
        }
        //----------Submit - Member---------------//
        $scope.member_add = function () {
            var params = {
                member_name: $scope.txtmember_name,
                member_designation: $scope.$parent.cbomember_designation.designation_type
            }
            var url = 'api/MstCustomerAdd/postmember';
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
                $scope.txtmember_name = '';
                $scope.cbomember_designation = '';
                member_list();
            });
        }
        $scope.deletemember = function (customer2member_gid) {
            var params =
                {
                    customer2member_gid: customer2member_gid
                }
            var url = 'api/MstCustomerAdd/DeleteMember';
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

                member_list();
            });

        }
        function member_list() {
            var url = 'api/MstCustomerAdd/GetMemberList';
            SocketService.get(url).then(function (resp) {
                $scope.member_list = resp.data.member_list;
                console.log(resp.data.member_list)
            });
        }

        //------Upload Photo----------//
        $scope.photo = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");
            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            localStorage.setItem($scope.uploadfrm, '$scope.uploadfrm');

            var url = 'api/MstCustomerAdd/Uploadphoto';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI();
                $("#adduploadphoto").val('');
                if (resp.data.status == true) {
                    $scope.hidephotodiv = false;
                    $scope.showphotodiv = true;
                }
                else {
                }
            });

        }
        //--------Individul Submit----------//
        $scope.individualsubmit = function () {
            if (($scope.txtindividualname == undefined)|| ($scope.txtindividualaadhar_no == undefined)  || ($scope.cboindividualuser_type == "")) {
                $scope.Warning = true;
            }
            else {
                $scope.Warning = false;

                var params = {
                    name: $scope.txtindividualname,
                    dob: $scope.txtdob,
                    age: $scope.txtage,
                    gender: $scope.rdbgender,
                    personalemail_address: $scope.txtpersonalemail_address,
                    officailemail_address: $scope.txtofficialemail_address,
                    telephone_no: $scope.txtindividualtelephoneno,
                    contact_person: $scope.txtcontactperson,
                    aadhar_no: $scope.txtindividualaadhar_no,
                    pan_no: $scope.txtindividualpan_no,
                    contactperson_designation: $scope.txtdesignation,
                    user_type: $scope.cboindividualuser_type.user_type,
                    usertype_gid: $scope.cboindividualuser_type.usertype_gid,
                    guarantor_id: $scope.txtindividualguarantor_id
                }
                var url = 'api/MstCustomerAdd/PostIndividualSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.addIndividual = false;
                        $scope.clickadd = false;
                        customer2userlist();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }
        }
        function customer2userlist() {
            var url = 'api/MstCustomerAdd/GetCustomer2UserDtl';
            SocketService.get(url).then(function (resp) {
                $scope.customer2user_list = resp.data.customer2userdtl_list;
                 });
        }
        $scope.institutionsubmit = function () {
            console.log($scope.txtcoi_date)
            if (($scope.txtinstitution_name == undefined)|| ($scope.txtinstitutiongst_no == undefined) || ($scope.cbouser_type.user_type == undefined) ) {
                $scope.Warning = true;
            }
            else {
                $scope.Warning = false;
                var params = {
                    name: $scope.txtinstitution_name,
                    coi_date: $scope.txtcoi_date,
                    telephone_no: $scope.txtinstitutiontelephoneno,
                    contact_person: $scope.txtinstitutioncontact_name,
                    contactperson_designation: $scope.txtinstitutioncontact_designation,
                    personalemail_address: $scope.txtcompanyemail_address,
                    company_type: $scope.cbocompanytype,
                    landmark: $scope.txtinstitutionlandmark,
                    year_business: $scope.txtinstitution_year,
                    month_business: $scope.txtinstitution_month,
                    credit_rating: $scope.txtinstitution_creditrating,
                    escrow: $scope.txtinstitution_escrow,
                    pan_no: $scope.txtinstitutionpan_no,
                    gst_no: $scope.txtinstitutiongst_no,
                    cin_no: $scope.txtinstitutioncin_no,
                    user_type: $scope.cbouser_type.user_type,
                    usertype_gid: $scope.cbouser_type.usertype_gid,
                    guarantor_id: $scope.txtinstitutionguarantor_id
                }
                console.log(params);
                var url = 'api/MstCustomerAdd/PostInstitutionSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.addinstitution = false;
                        $scope.clickadd = false;
                        customer2userlist();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }
        // -------Customer Submit-------//
        $scope.customerSubmit = function () {
            if ($scope.rdbassociate=='Yes')
            {
               var sa_idname= $scope.cbosa_idname.name;
               var sa_id_gid = $scope.cbosa_idname.associatemaster_gid;
               var sa_payout = $scope.txtsa_payout;
            }
            else{
                var sa_idname = '';
                var sa_id_gid = '';
                var sa_payout = '';
            }
            var zonlRM_name = $('#zonlRM_name :selected').text();
            var riskmanager_name = $('#riskmanager_name :selected').text();
            var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();
            var params = {
                customer_urn: $scope.txtcustomer_urn,
                vertical_code: $scope.cbovertical.vertical_code,
                vertical_gid: $scope.cbovertical.vertical_gid,
                constitution_name: $scope.cboconstitution.constitution_name,
                constitution_gid: $scope.cboconstitution.constitution_gid,
                businessunit_name: $scope.cbobusinessunit.businessunit_name,
                businessunit_gid: $scope.cbobusinessunit.businessunit_gid,
                primaryvaluechain_list: $scope.$parent.cboprimaryvalue_chain,
                secondaryvaluechain_list: $scope.$parent.cbosecondaryvalue_chain,
                SA_status: $scope.rdbassociate,
                sa_idname:sa_idname,
                sa_id_gid:sa_id_gid,
                sa_payout:sa_payout,
                relationshipmgmt_name: $scope.cborelationshipMgmt.employee_name,
                relationshipMgmtGid: $scope.cborelationshipMgmt.employee_gid,
                businesshead_name: $scope.cbobusinessHead.employee_name,
                businessHeadGid: $scope.cbobusinessHead.employee_gid,
                zonal_name: $scope.cbozonalHead.employee_name,
                zonalGid: $scope.cbozonalHead.employee_gid,
                cluster_manager_name: $scope.cboclustermanager.employee_name,
                clustermanagerGid: $scope.cboclustermanager.employee_gid,
                creditmanager_name: $scope.cbocreditmanager.employee_name,
                creditmanagerGid: $scope.cbocreditmanager.employee_gid,
                zonal_riskmanagerGID: $scope.cbozonalrm_gid.employee_gid,
                zonal_riskmanagerName: zonlRM_name,
                risk_managerGID: $scope.cboriskmanager_gid.employee_gid,
                riskmanager_name: riskmanager_name,
                riskMonitoring_GID: $scope.cboiskMonitoring_gid.employee_gid,
                riskMonitoring_Name: RiskMonitoring_Name,
                ccmail: $scope.txtccmail,
                major_corporate: $scope.txtmajor_corporate
            }
        console.log(params)
            var url = 'api/MstCustomerAdd/PostCustomerSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCustomerSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.onchangeddob = function () {
            var params = {
                dob: $scope.txtdob
            }
            console.log(params);
            var url = 'api/MstCustomerAdd/GetAge';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtage = resp.data.age;
            });
        }
        //---------- View nIformation----------//
        $scope.viewuserdtl = function (customer2usertype_gid) {
            $scope.customer2usertype_gid = customer2usertype_gid;
            $scope.customer2usertype_gid = localStorage.setItem('customer2usertype_gid', customer2usertype_gid);
            $location.url('app/MstCustomer2userView?lsredirect=add');
        }
        $scope.aadharnovalidation = function () {
            $scope.Warning = false;

            var params =
                {
                    aadhar_no: $scope.txtindividualaadhar_no,

                }
            var url = 'api/MstCustomerAdd/GetIndividualInformation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.address_list = resp.data.address_list;
                $scope.idproof_list = resp.data.idproof_list;
                $scope.mobileno_list = resp.data.mobileno_list;
                $scope.member_list = resp.data.member_list;
                $scope.txtage = resp.data.age;
                //$scope.lblphoto_path = resp.data.photo_path;
                //$scope.lblphoto_name = resp.data.photo_name;
                //$scope.lblpan_no = resp.data.pan_no;
                // $scope.txta = resp.data.aadhar_no;
                $scope.txtcontact_person = resp.data.contact_person;
                $scope.txttelephone_no = resp.data.telephone_no;
                $scope.txtofficailemail_address = resp.data.officailemail_address;
                $scope.txtpersonalemail_address = resp.data.personalemail_address;
                $scope.rdbgender_individual = resp.data.gender;
                $scope.txtdob = resp.data.dob;
                $scope.txtdob = Date.parse($scope.txtdob);

                $scope.txtdob = resp.data.dob;
                $scope.txtindividualname = resp.data.name;
                $scope.lbluser_type = resp.data.user_type;
                $scope.txtindividualguarantor_id = resp.data.guarantor_id;
            });

        }
        $scope.gstno_validation = function () {
            $scope.Warning = false;
            var params =
                {
                    gst_no: $scope.txtinstitutiongst_no,

                }
            console.log(params);
            var url = 'api/MstCustomerAdd/GetInstitutionInformation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.address_list = resp.data.address_list;
                $scope.idproof_list = resp.data.idproof_list;
                $scope.mobileno_list = resp.data.mobileno_list;
                $scope.member_list = resp.data.member_list;
                //$scope.txtcustomer_type = resp.data.customer_type;
                //  $scope.lblgst_no = resp.data.gst_no;
                $scope.txtinstitution_year = resp.data.year_business;
                $scope.cbocompanytype = resp.data.company_type;
                $scope.txtinstitutioncontact_designation = resp.data.contactperson_designation;
                $scope.txtinstitutioncin_no = resp.data.cin_no;
                // $scope.lblcin_date = resp.data.cin_date;
                $scope.txtinstitutionlandmark = resp.data.landmark;
                $scope.txtinstitution_month = resp.data.month_business;
                $scope.txtinstitution_creditrating = resp.data.credit_rating;
                $scope.txtinstitution_escrow = resp.data.escrow;
                //$scope.txtage = resp.data.age;
                //$scope.lblphoto_path = resp.data.photo_path;
                //$scope.lblphoto_name = resp.data.photo_name;
                $scope.txtinstitutionpan_no = resp.data.pan_no;
                // $scope.txta = resp.data.aadhar_no;
                $scope.txtinstitutioncontact_name = resp.data.contact_person;
                $scope.txtinstitutiontelephoneno = resp.data.telephone_no;
                $scope.txtcompanyemail_address = resp.data.personalemail_address;
                $scope.txtinstitution_name = resp.data.name;
                $scope.lbluser_type = resp.data.user_type;
                $scope.txtcoi_date = resp.data.cin_date;
                $scope.txtcoi_date = Date.parse($scope.txtcoi_date);
                $scope.txtinstitutionguarantor_id = resp.data.guarantor_id;
            });

        }
    }

})();