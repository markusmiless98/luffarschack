//refactor for building form when button clicked

const all_pop_up_check = document.getElementById("skip_all_popup");

export function BuildPopUp(messageText, confirmText, onConfirm, cancelText, onCancel,) {
	if (all_pop_up_check == "Yes")
	{
		onConfirm();
		return;
	}

    const dialog = document.createElement("dialog");
    const message = document.createElement("p");
    const form = document.createElement("form");
    const buttonYes = document.createElement("button");

    message.textContent = messageText;
    buttonYes.value = confirmText;
    buttonYes.textContent = confirmText;

    if(cancelText)//if cancelText has value only then add text context
    {
        let buttonNo = document.createElement("button");
        buttonNo.value = cancelText;
        buttonNo.textContent = cancelText;
        form.append(buttonYes, buttonNo);
    }
    else{
        form.append(buttonYes);
    } 

    form.method = "dialog"; //the value from the form is sent to dialog.returnValue, can have the value be sent to something else
    dialog.append(message, form);

    dialog.addEventListener("close", () => {
        if (dialog.returnValue === confirmText) {
            onConfirm();
        }
        if (dialog.returnValue === cancelText) {
            onCancel();
        }

        console.log(dialog.returnValue);
        dialog.remove();
    });


    document.body.appendChild(dialog);
    dialog.showModal();
}

//example usage
export function test(){
const test = document.createElement("div");
const button = document.createElement("button");
const button2 = document.createElement("button");

button.textContent = "YesNo"
button2.textContent = "YesOnly"

button.addEventListener("click",
    () => BuildPopUp(
       
        "message?",
        "yes",
        () => console.log("yes works"), //put what you want to run when user presses confirmation
        "no",
        () => console.log("no works"))); //put what you want to run when user presses cancel


button2.addEventListener("click",
    () => BuildPopUp(
        "message?",
        "yes",
        () => console.log("yes works")));//you can exlude the cancel value and you wont have that button appear

test.append(button, button2)
document.body.appendChild(test);
}






