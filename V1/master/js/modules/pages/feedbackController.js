(function () {
    'use strict';

    angular
        .module('angle')
        .controller('feedbackController', feedbackController);

    feedbackController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies','$timeout'];

    function feedbackController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies,$timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'feedbackController';
        var lblRatingText = document.getElementById('lblRatingText');
        var url = window.location.href;
        var relPath = url.split("?id=");
        var relpath1 = relPath[1];


        activate();

        function activate() {
            $scope.showfeedback = true;
            $scope.hidefeedback = true;
            $scope.hideexitfeedback = true;


            var params = {
                token: relpath1
            };
            var url = 'api/FeedBack/GetFeedbackDtl';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.ticket_details = resp.data;
                    $scope.hidefeedback = true;
                    $scope.showfeedback = true;
                    $scope.hideexitfeedback = true;
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    $scope.hideexitfeedback = false;
                    $scope.showfeedback = false;
                    $scope.hidefeedback = true;
                }
            });
        }
        const stars = document.querySelector(".ratings").children;
        var ratingValue = document.querySelector("#rating-count")
        //ratingValue.value = 5;
        //lblRatingText.innerHTML = "Very Good";
        //lblRatingText.style.color = 'Green';
        //for (let i = 0; i < 5; i++) {
        //    stars[i].classList.remove("fa-star-o");
        //    stars[i].classList.add("fa-star");
        //}
        let index;
        for (let i = 0; i < stars.length; i++) {
            stars[i].addEventListener("mouseover", function () {
                for (let j = 0; j < stars.length; j++) {
                    stars[j].classList.remove("fa-star");
                }
                for (let j = 0; j <= i; j++) {
                    stars[j].classList.remove("fa-star-o");
                    stars[j].classList.add("fa-star");
                }
            })
            stars[i].addEventListener("click", function () {
                ratingValue.value = i + 1;
                index = i;
                var val = i + 1;           
                if (val == 5) {
                    lblRatingText.innerHTML = "Very Good";
                    lblRatingText.style.color = 'Green';
                }
                else if (val == 4) {
                    lblRatingText.innerHTML = "Good";
                    lblRatingText.style.color = 'Green';
                }
                else if (val == 3) {
                    lblRatingText.innerHTML = "Ok";
                    lblRatingText.style.color = 'Orange';
                }
                else if (val == 2) {
                    lblRatingText.innerHTML = "Poor";
                    lblRatingText.style.color = 'red';
                }
                else {
                    lblRatingText.innerHTML = "Very Poor";
                    lblRatingText.style.color = 'red';
                }
               
            })
            stars[i].addEventListener("mouseout", function () {
                for (let j = 0; j < stars.length; j++) {
                    stars[j].classList.remove("fa-star");
                    stars[j].classList.add("fa-star-o");
                }
                for (let j = 0; j <= index; j++) {
                    stars[j].classList.remove("fa-star-o");
                    stars[j].classList.add("fa-star");
                }
            })


        }


        /* Feedback Add */


        $scope.feedback_submit = function () {
          
            if (ratingValue.value == undefined) {
                $scope.req_star = true;
                $timeout(function () {
                    $scope.req_star = false;
                }, 4000);
                return false;
            }

            var params = {
                rating_count: ratingValue.value,
                feedback_remarks: $scope.txtremarks,
                rating_text: lblRatingText.innerHTML,
                token: relpath1
            }

         
          

            console.log(params);
            var url = 'api/FeedBack/PostFeedbackdtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });

                    $scope.hidefeedback = false;
                    $scope.showfeedback = false;
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });

                    $scope.showfeedback = true;
                    $scope.hidefeedback = true;
                }
            });

        }

    }
})();
