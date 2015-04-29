module App.Scope {
    "use strict";

    export class AlertScope {
        public alerts: Array<Alert> = []

        public addAlert(message: string) {
            this.alerts.push(new Alert("success", message));
        }
    
        public closeAlert(index: number) {
            this.alerts.splice(index, 1);
        }
    }
}  