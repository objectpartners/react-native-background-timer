using ReactNative.Modules.Core;
using System.Threading.Thread;

namespace ReactNative.Modules.SQLite
{
    public class BackgroundTimerModule : ReactContextNativeModuleBase
    {
        private Thread backgroundThread;
        private ReactContext reactContext;

        public BackgroundTimerModule(ReactApplicationContext reactContext) {
            super(reactContext);
            this.reactContext = reactContext;
        }

        public override string Name
        {
            get
            {
                return "BackgroundTimer";
            }

        }

        [ReactMathod]
        public void start(final int delay) {
            Task.Delay(delay).ContinueWith(_ =>
                backgroundThread = new Thread(_ => 
                    IsBackground = true;
                    sendEvent(reactContext, "backgroundTimer");
                );
                thread.Start();
                thread.Join();
            );
        }

        [ReactMethod]
        public void stop() {
            // avoid null pointer exception when stop is called without start
            if (backgroundThread != null) backgroundThread.interrupt();
        }

        private void sendEvent(ReactContext reactContext, String eventName) {
            reactContext
            .getJSModule(DeviceEventManagerModule.RCTDeviceEventEmitter.class)
            .emit(eventName, null);
        }

        [ReactMethod]
        public void setTimeout(final int id, final int timeout) {
            Task.Delay(timeout).ContinueWith(_ =>
                Thread thread = new Thread(_ => 
                    if (getReactApplicationContext().hasActiveCatalystInstance()) {
                        getReactApplicationContext()
                            .getJSModule(DeviceEventManagerModule.RCTDeviceEventEmitter.class)
                            .emit("backgroundTimer.timeout", id);
                    }
                );
                thread.Start();
                thread.Join();
            );
        }

        /*[ReactMethod]
        public void clearTimeout(final int id) {
            // todo one day..
            // not really neccessary to have
        }*/
    }
}