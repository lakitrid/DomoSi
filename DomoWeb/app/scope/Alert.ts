module App.Scope {
    "use strict";

    export class Alert {
        public type: string
        public msg: string

        constructor(private _type: string, private _msg: string) {
            this.type = _type;
            this.msg = _msg;
        }
    }
}  