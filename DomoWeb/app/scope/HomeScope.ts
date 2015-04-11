module App.Scope {
    "use strict";

    export class HomeScope {
        constructor(private $cookieStore: angular.cookies.ICookieStoreService) {

        }

        public toggle: boolean

        public getWidth() {
            return window.innerWidth;
        }

        public toggleSidebar() {
            this.toggle = !this.toggle;
            this.$cookieStore.put('toggle', this.toggle);
        }
    }
} 