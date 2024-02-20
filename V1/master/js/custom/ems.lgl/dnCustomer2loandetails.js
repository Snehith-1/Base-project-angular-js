(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dnCustomer2loandetailscontroller', dnCustomer2loandetailscontroller);

    dnCustomer2loandetailscontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function dnCustomer2loandetailscontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dnCustomer2loandetailscontroller';

        activate();

        function activate() {
            $scope.guarantorinfo = false;
            $scope.Sanctioninfo = false;

            $scope.urn = localStorage.getItem('urn');
            var url = 'api/lglTrnDn2CustomerDetails/Getcustomerupdatedetails';
            var param = {
                urn: $scope.urn
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customerCode = resp.data.customerCodeedit;
                $scope.customerName = resp.data.customerNameedit;
                $scope.contactPerson = resp.data.contactPersonedit;
                $scope.mobileNo = resp.data.mobileNo_edit;
                $scope.contactno = resp.data.contactno_edit;
                $scope.email = resp.data.emailedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.region = resp.data.regionedit;
                $scope.country = resp.data.countryedit;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;
                $scope.riskmanager = resp.data.risk_managernameedit;
                $scope.district_name = resp.data.district_nameedit;
                $scope.postalcode = resp.data.postalcode_edit;
                $scope.tomail = resp.data.tomailedit;
                $scope.ccmail = resp.data.ccmailedit;
                $scope.zonalHead = resp.data.zonal_name;
                $scope.businessHead = resp.data.businesshead_name;
                $scope.clustermanager = resp.data.cluster_manager_name;
                $scope.creditmanager = resp.data.creditmanager_name;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_name;
                $scope.customerURN = resp.data.customer_urnedit;
                $scope.pan_number = resp.data.pan_number;
                $scope.gst_number = resp.data.gst_number;
                $scope.txtmajor_corporate = resp.data.major_corporateedit;
                $scope.cboconstitution = resp.data.constitution_gidedit;
                $scope.cboconstitutionname = resp.data.constitution_nameedit;
                unlockUI();
            });
            var url = 'api/lglTrnDn2CustomerDetails/Getcustomerdetails';
            var param = {
                urn: $scope.urn
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.lblpan_number = resp.data.pan_no;
                $scope.customer2userdtl_list = resp.data.customer2userdtl_list;
                $scope.address_list = resp.data.address_list;
                $scope.idproof_list = resp.data.idproof_list;
                $scope.mobileno_list = resp.data.mobileno_list;
                $scope.member_list = resp.data.member_list;
                $scope.lblcustomer_type = resp.data.customer_type;
                $scope.lblgst_no = resp.data.gst_no;
                $scope.lblyear_business = resp.data.year_business;
                $scope.lblcompany_type = resp.data.company_type;
                $scope.lblcontactperson_designation = resp.data.contactperson_designation;
                $scope.lblcin_no = resp.data.cin_no;
                $scope.lblcin_date = resp.data.cin_date;
                $scope.lbllandmark = resp.data.landmark;
                $scope.lblmonth_business = resp.data.month_business;
                $scope.lblcredit_rating = resp.data.credit_rating;
                $scope.lblescrow = resp.data.escrow;
                $scope.lblage = resp.data.age;
                $scope.lblphoto_path = resp.data.photo_path;
                $scope.lblphoto_name = resp.data.photo_name;
                $scope.lblaadhar_no = resp.data.aadhar_no;
                $scope.lblcontact_person = resp.data.contact_person;
                $scope.lbltelephone_no = resp.data.telephone_no;
                $scope.lblofficailemail_address = resp.data.officailemail_address;
                $scope.lblpersonalemail_address = resp.data.personalemail_address;
                $scope.lblgender = resp.data.gender;
                $scope.lbldob = resp.data.dob;
                $scope.lblname = resp.data.name;
                $scope.lbluser_type = resp.data.user_type;
                unlockUI();
            });
            var url = 'api/lglTrnDn2CustomerDetails/GetGuarantordetails';
            var param = {
                urn: $scope.urn
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.additional_list = resp.data.customer2userdtl_list;
            });
            var url = "api/lglTrnDn2CustomerDetails/Getsanctionloandetails";
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sanctiondetails = resp.data.sanctionloanListurn;
                $scope.sanctionDocument = resp.data.upload_listurn;
                var previstdocumentflag;
                if (resp.data.upload_list == null) {
                    $scope.previstdocumentflag = 'N';
                }
                else {
                    $scope.previstdocumentflag = 'Y';
                }
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid
                    };
                    var url = 'api/lglTrnDn2CustomerDetails/GetloanListDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanListurn;
                        value.expand = false;

                    });
                });
            });

            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
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
            $scope.urn = localStorage.getItem('urn');
            $scope.initiatedn1 = true;
            $scope.otherloan = true;
            $scope.click = true;
            $scope.dn1format = true;
            $scope.button = true;
            $scope.courier_info = false;
            $scope.revert = true;
            $scope.sanctiondtl = true;
            var url = "api/misDataimport/getcustomer2Loan"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.mdlMisdataimport;
                $scope.customer_name = resp.data.customer_name;
                $scope.DN1status = resp.data.DN1status;
                $scope.dn_status = resp.data.dn_status;
                console.log(resp.data.dn_status);
                if ((resp.data.DN1status == 'DN1 Sent')) {
      
                    $scope.info = false;
                    $scope.initiatedn1 = true;
                }
                if ((resp.data.DN1status == 'DN1 Sent') || (resp.data.DN1status == 'DN1 Skip')) {
                    $scope.data = true;
                }
            });
            var url = "api/misDataimport/getDN1Status"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dnhistory_list = resp.data.dnhistory_list;
            });
            var url = "api/misDataimport/DN1ContentDTL"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.content = resp.data.template_content;
                document.getElementById('test').innerHTML += $scope.content;
                $scope.DN1_status = resp.data.DN1_status;

                if ((resp.data.DN1_status == 'DN1 Generated')) {

                    $scope.dn1format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
                if ((resp.data.DN1_status == 'DN1 Hold')) {
                    $scope.dn1format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
                if (resp.data.DN1_status == 'DN1 Reverted') {
                    $scope.initiatedn1 = true;
                    $scope.otherloan = true;
                    $scope.click = true;
                    $scope.dn1format = true;
                    $scope.button = true;
                    $scope.courier_info = false;
                    $scope.revert = false;
                }
            });

            var url = "api/misDataimport/getcourierinfo"
            lockUI();
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.couriered_by = resp.data.couriered_by;
                $scope.courier_center = resp.data.courier_center;
                $scope.courier_date = resp.data.courier_date;
                $scope.courier_refno = resp.data.courier_refno;
                $scope.courier_remarks = resp.data.courier_remarks;
                $scope.courier_status = resp.data.dn1courier_status;
            });
            var url = "api/misDataimport/Getrevertdetails"
            lockUI();
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.updated_date = resp.data.updated_date;
                $scope.updated_by = resp.data.updated_by;
                $scope.dn_status = resp.data.dn_status;
                $scope.remarks = resp.data.remarks;
            });
        }
        $scope.onselectedchangeusertype=function()
        {
            console.log($scope.cbousertype);
            if ($scope.cbousertype == 'guarantor') {
                $scope.guarantorinfo = true;
                var param = {
                    urn: localStorage.getItem('urn')
                }
                var url = 'api/lglTrnDn2CustomerDetails/GetGuarantorlist';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.guarantor_list = resp.data.customer2userdtl_list;
                });
            }
            else {
                $scope.guarantorinfo = false;
            }
        }
        $scope.checkall = function (selected) {
            angular.forEach($scope.documentlist_gid, function (val) {
                val.checked = selected;
            });
        }
        // Numeric to Word - Indian Standard...//

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.DN1send = function () {
            var url = "api/misDataimport/DN1Status"
            var param = {
                urn: $scope.urn,
                courier_refno: $scope.txtcourierefno,
                courier_center: $scope.txtcouriercenter,
                courier_date: $scope.txtcourierdate,
                couriered_by: $scope.txtcourierby,
                courier_remarks: $scope.txtcourieredremarks

            };
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('DN1 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN1 Status ')
                }
                $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                activate();
            });
            $scope.courier_info = false;

            $scope.info = true;
        }
        $scope.dn1generate = function () {
            $scope.courier_info = true;
            $scope.info = true;
           
            
            var url = "api/misDataimport/DN1generate";
            lockUI();
            var param = {
                urn: $scope.urn,
                template_content: $scope.content
            };

            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();

                    Notify.alert('DN1 Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN1 Status ')
                }
                $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                activate();
            });
           
        }
        $scope.dn2generate = function () {
            $scope.courier_info = true;
            $scope.info = true;

            console.log('dn2');
            var url = "api/misDataimport/DN2generate";
            lockUI();
            var param = {
                urn: $scope.urn,
                template_content: $scope.content
            };

            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();

                    Notify.alert('DN2 Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN2 Status ')
                }
                $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                activate();
            });

        }
        $scope.dn3generate = function () {
            $scope.courier_info = true;
            $scope.info = true;
            console.log('test')

            var url = "api/misDataimport/DN3generate";
            lockUI();
            var param = {
                urn: $scope.urn,
                template_content: $scope.content
            };

            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();

                    Notify.alert('DN3 Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN3 Status ')
                }
                $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                activate();
            });

        }
        $scope.DN1skip = function () {
            var url = "api/misDataimport/DN1skip"
            var param = {
                urn: $scope.urn
            };
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('DN1 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN1 Status ')
                }
                activate();
            });
        }
        $scope.Dn1back = function () {
            $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
        }
        $scope.clickinitiatedn1 = function () {

            $scope.sanctiondtl = false;
            $scope.initiatedn1 = true;
            $scope.courier_info = false;
            var url = 'api/misDataimport/Getsanctionloandetails';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtdnsanctionref_no = resp.data.sanction_refno;
                $scope.txtdnsanction_date = resp.data.sanction_date;
                $scope.txtdnsanction_amount = resp.data.sanction_amount;
                $scope.txtdnref_no = "SAMFIN/RMD/";

            });

            var url = 'api/lglTrnDn2CustomerDetails/template_list';
          
            SocketService.get(url).then(function (resp) {

                $scope.template_list = resp.data.template_list;
               
            });
            var url = 'api/misDataimport/GetSanctiondtl';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {
                 $scope.dnref_no = resp.data.dn1ref_no;
                $scope.dnsanctionref_no = resp.data.dn1sanctionref_no;
                $scope.dnsanction_date = resp.data.dn1_date;
                $scope.dn_type = resp.data.dn_type;
                var amount_dn1 = new Intl.NumberFormat('en-IN').format(resp.data.dn1sanction_amount);
                $scope.dnsanction_amount = amount_dn1;
                $scope.dn_flag = resp.data.dn_flag;
                if (resp.data.dn1_flag == "N") {
                    $scope.initiatedn1 = true;
                    $scope.sanctiondtl = false;
                }
                if (resp.data.dn1_flag == "Y") {
                    $scope.initiatedn1 = false;
                    $scope.sanctiondtl = true;
                }

            });

            var url = 'api/lglTrnDn2CustomerDetails/DN1Content';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.content = resp.data.template_content;

            });

            $scope.info = true;

        }
        $scope.close = function () {
            $scope.courier_info = false;
            $scope.initiatedn1 = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
        }
        $scope.cancel=function()
        {
            var url = 'api/lglTrnDn2CustomerDetails/DN1Cancel';
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                Notify.alert('Sanction Information canceled', {
                    status: 'info',
                    pos: 'top-center',
                    timeout: 3000
                });
            });
            $scope.courier_info = false;
            $scope.initiatedn1 = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
            $scope.cbousertype = '';
        }
        $scope.revert = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/revertdn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.revert_yes = function () {

                    var params = {

                        urn: urn,
                        remarks: $scope.txtremarks
                    }
                    console.log(params);
                    var url = 'api/misDataimport/RevertDN1';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                        activate();
                    });
                }
            }
        }
        $scope.hold = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/holddn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.hold_yes = function () {

                    var params = {

                        urn: urn,
                        remarks: $scope.txtremarks
                    }
                    console.log(params);
                    var url = 'api/misDataimport/HoldDN1';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                        activate();
                    });
                }
            }
        }
        $scope.unhold = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/unholddn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.unhold_yes = function () {

                    var params = {

                        urn: urn,
                    }
                    console.log(params);
                    var url = 'api/misDataimport/UnholdDN1';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        $location.url('app/LglTrnDNTrackerAE?lstab=dn1tracker');
                        activate();
                    });
                }
            }
        }

        $scope.dnsanctionsubmit = function () {
           
            if ($scope.cbousertype == 'guarantor')
            {
                if(($scope.cboguarantor=='')||($scope.cboguarantor==undefined))
                {
                    $scope.Sanctioninfo = true;
                }
            }
            else {
                $scope.Sanctioninfo = false;
            if ($scope.cbousertype == 'guarantor')
            {
                var guarantor_name = $scope.cboguarantor.name;
                var guarantor_gid = $scope.cboguarantor.customer2usertype_gid

            }
            else {
                var guarantor_name = '';
                var guarantor_gid = '';
            }
            var param = {
                urn: $scope.urn,
                dnsanctionref_no: $scope.txtdnsanctionref_no,
                dnsanction_date: $scope.txtdnsanction_date,
                dnsanction_amount: $scope.txtdnsanction_amount,
                dnref_no: $scope.txtdnref_no,
                user_type: $scope.cbousertype,
                template_type: $scope.cbotemplate.template_name,
                template_gid: $scope.cbotemplate.template_gid,
                guarantor_name: guarantor_name,
                guarantor_gid: guarantor_gid
            };
                
            if ($scope.cbotemplate.template_name == 'Legal-DN1')
            {

           
             var url = 'api/lglTrnDn2CustomerDetails/PostDN1Sanctiondtl';
             lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn
                    };
                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dnref_no = resp.data.dn1ref_no;
                        $scope.dnsanctionref_no = resp.data.dn1sanctionref_no;
                        $scope.dnsanction_date = resp.data.dn1_date;
                        $scope.dn_type = resp.data.dn_type;
                        console.log(resp.data.dn1sanction_amount);
                        var amount_dn1 = new Intl.NumberFormat('en-IN').format(resp.data.dn1sanction_amount);
                        $scope.dnsanction_amount = amount_dn1;
                        $scope.dn1_flag = resp.data.dn1_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                        if (resp.data.dn1_flag == "Y") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                    });

                    var url = 'api/lglTrnDn2CustomerDetails/DN1Content';
                    var param = {
                        urn: $scope.urn

                    };
                    console.log(param);
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.content = resp.data.template_content;

                    });

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


            });
            }
            
           
        else if($scope.cbotemplate.template_name == 'Legal-DN2')
        {
            var url = 'api/lglTrnDn2CustomerDetails/PostDN2Sanctiondtl';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn
                    };
                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dnref_no = resp.data.dn2ref_no;
                        $scope.dnsanctionref_no = resp.data.dn2sanctionref_no;
                        $scope.dnsanction_date = resp.data.dn2_date;
                        $scope.dn_type = resp.data.dn_type;
                        var amount_dn1 = new Intl.NumberFormat('en-IN').format(resp.data.dn2sanction_amount);
                        $scope.dnsanction_amount = amount_dn1;
                        $scope.dn2_flag = resp.data.dn2_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                        if (resp.data.dn2_flag == "Y") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                    });

                    var url = 'api/lglTrnDn2CustomerDetails/DN2Content';
                    var param = {
                        urn: $scope.urn

                    };
                    console.log(param);
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.content = resp.data.template_content;

                    });

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


            });
        }
        else if ($scope.cbotemplate.template_name == 'Legal-DN3') {
            var url = 'api/lglTrnDn2CustomerDetails/PostDN3Sanctiondtl';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn
                    };
                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dnref_no = resp.data.dn3ref_no;
                        $scope.dnsanctionref_no = resp.data.dn3sanctionref_no;
                        $scope.dnsanction_date = resp.data.dn3_date;
                        $scope.dn_type = resp.data.dn_type;
                        var amount_dn1 = new Intl.NumberFormat('en-IN').format(resp.data.dn3sanction_amount);
                        $scope.dnsanction_amount = amount_dn1;
                        $scope.dn3_flag = resp.data.dn3_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                        if (resp.data.dn3_flag == "Y") {
                            $scope.initiatedn1 = false;
                            $scope.sanctiondtl = true;
                        }
                    });

                    var url = 'api/lglTrnDn2CustomerDetails/DN3Content';
                    var param = {
                        urn: $scope.urn

                    };
                    console.log(param);
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.content = resp.data.template_content;

                    });

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


            });
        }
        }
        }
        $scope.dn1cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn1 = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
            $scope.txtdnsanctionref_no = '';
            $scope.txtdnsanction_date = '';
            $scope.txtdnsanction_amount = '';
            $scope.txtdnref_no = '';
        }
        $scope.amountschange = function () {

            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount = inWords(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdnsanction_amount = "";

            }
            else {
                document.getElementById('sanctionamount_words').innerHTML = lswords_sanctionamount;
                $scope.txtdnsanction_amount = output;
            }
        }
    }
})();