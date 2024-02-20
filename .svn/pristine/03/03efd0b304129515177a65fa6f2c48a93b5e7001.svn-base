(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCibilEditController', MstCibilEditController);

    MstCibilEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCibilEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCibilEditController';

        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };
            $scope.cibildatadtl_gid = localStorage.getItem('cibildatadtl_gid');
            lockUI();
            var params = {
                cibildatadtl_gid: $scope.cibildatadtl_gid
            }
            var url = "api/MstCibilData/GetEditCibildata";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtname = resp.data.name;
                $scope.txtsubmission_type = resp.data.submission_type;
                $scope.txtsubmitted_on = resp.data.txtsubmitted_on;
                   $scope.txtsubmitted_on = (resp.data.txtsubmitted_on);
                 if (resp.data.closed_on != '0001-01-01T00:00:00' || resp.data.closed_on != '' || resp.data.closed_on != null) {
                    $scope.txtclosed_on = new Date(resp.data.closed_on);
                 }
                 else {
                     $scope.txtclosed_on = '';
                 }

                console.log(resp.data.closed_on);
              //  $scope.txtclosed_on = new Date(resp.data.closed_on);
                $scope.txtindicator = resp.data.indicator;
                $scope.txtaccount_no = resp.data.account_no;
                $scope.txtcurrent_balance = resp.data.current_balance;
                $scope.txtoverdue_amount = resp.data.overdue_amount;
                $scope.txtodbucket_01 = resp.data.odbucket_01;
                $scope.txtodbucket_02 = resp.data.odbucket_02;
                $scope.txtodbucket_03 = resp.data.odbucket_03;
                $scope.txtodbucket_04 = resp.data.odbucket_04;
                $scope.txtodbucket_05 = resp.data.odbucket_05;
                $scope.txtod_days = resp.data.od_days;
                $scope.txtaccount_status = resp.data.account_status;
                $scope.txtcibil_status = resp.data.cibil;
                $scope.txthighmark_status = resp.data.highmark;
                $scope.txtexperian_status = resp.data.experian;
                $scope.txtequifax_status = resp.data.euifax;
            });
        }
        $scope.cibilUpdate=function()
        {
            var params = {
                submission_type: $scope.txtsubmission_type,
                submittedon: $scope.txtsubmitted_on,
                current_balance: $scope.txtcurrent_balance,
                overdue_amount: $scope.txtoverdue_amount,
                odbucket_01: $scope.txtodbucket_01,
                odbucket_02: $scope.txtodbucket_02,
                odbucket_03: $scope.txtodbucket_03,
                odbucket_04: $scope.txtodbucket_04,
                odbucket_05: $scope.txtodbucket_05,
                od_days: $scope.txtod_days,
                account_status: $scope.txtaccount_status,
                closedon: $scope.txtclosed_on,
                cibil: $scope.txtcibil_status,
                highmark: $scope.txthighmark_status,
                experian: $scope.txtexperian_status,
                euifax: $scope.txtequifax_status,
                cibildatadtl_gid:$scope.cibildatadtl_gid
            }
            var url = 'api/MstCibilData/PostCibilData';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $state.go('app.MstCibilDataSummary');
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    Notify.alert(resp.data.message,'warning')
                }
            });
        }
       
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtoverdue_amount = "";
            }
            else {
                document.getElementById('sanctionamount_words').innerHTML = lswords_sanctionamount;
                $scope.txtoverdue_amount = output;
            }
        }
    
        $scope.amountschange1 = function () {
            var input = document.getElementById('txtInput1').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount1 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcurrent_balance = "";
            }
            else {
                document.getElementById('sanctionamount_words1').innerHTML = lswords_sanctionamount1;
                $scope.txtcurrent_balance = output;
            }
        }

        $scope.amountschange2 = function () {
            var input = document.getElementById('txtInput2').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount2 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodbucket_01 = "";
            }
            else {
                document.getElementById('sanctionamount_words2').innerHTML = lswords_sanctionamount2;
                $scope.txtodbucket_01 = output;
            }
        }

        $scope.amountschange3 = function () {
            var input = document.getElementById('txtInput3').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount3 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodbucket_02 = "";
            }
            else {
                document.getElementById('sanctionamount_words3').innerHTML = lswords_sanctionamount3;
                $scope.txtodbucket_02 = output;
            }
        }

        $scope.amountschange4 = function () {
            var input = document.getElementById('txtInput4').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount4 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodbucket_03 = "";
            }
            else {
                document.getElementById('sanctionamount_words4').innerHTML = lswords_sanctionamount4;
                $scope.txtodbucket_03 = output;
            }
        }

        $scope.amountschange5 = function () {
            var input = document.getElementById('txtInput5').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount5 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodbucket_04 = "";
            }
            else {
                document.getElementById('sanctionamount_words5').innerHTML = lswords_sanctionamount5;
                $scope.txtodbucket_04 = output;
            }
        }

        $scope.amountschange6 = function () {
            var input = document.getElementById('txtInput6').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount6 = cmnfunctionService.fnConvertNumbertoWord(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodbucket_05 = "";
            }
            else {
                document.getElementById('sanctionamount_words6').innerHTML = lswords_sanctionamount6;
                $scope.txtodbucket_05 = output;
            }
        }
        $scope.cibilback = function () {
            $state.go('app.MstCibilDataSummary')
        }
    }
})();
