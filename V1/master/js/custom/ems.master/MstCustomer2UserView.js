(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCustomer2userView', MstCustomer2userView);

    MstCustomer2userView.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function MstCustomer2userView($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCustomer2userView';
        activate();

        function activate() {
            var url = window.location.href;
            var relPath = url.split("lsredirect=");
            $scope.relpath1 = relPath[1];
            var params = {
                customer2usertype_gid: localStorage.getItem('customer2usertype_gid')
            }
            var url = 'api/MstCustomerAdd/GetCustomer2UserInfo';
            SocketService.getparams(url, params).then(function (resp) {
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
                $scope.lblpan_no = resp.data.pan_no;
                $scope.lblaadhar_no = resp.data.aadhar_no;
                $scope.lblcontact_person = resp.data.contact_person;
                $scope.lbltelephone_no = resp.data.telephone_no;
                $scope.lblofficailemail_address = resp.data.officailemail_address;
                $scope.lblpersonalemail_address = resp.data.personalemail_address;
                $scope.lblgender = resp.data.gender;
                $scope.lbldob = resp.data.dob;
                $scope.lblname = resp.data.name;
                $scope.lbluser_type = resp.data.user_type;
                
            });
        }
        $scope.back = function (relpath1)
        {
            if (relpath1 == "view")
            {
                $state.go('app.MstCustomerView');
            }
            else {
                $location.url('app/MstCustomeradd?lsredirect=add');
              
            }
            
        }
    }
})();
