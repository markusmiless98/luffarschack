//refactor for building form when button clicked
function popUpsDisabled() {
    return document.getElementById("skip_all_popup")?.value === "Yes";
}

const global_pop_ups_disabled = () => document.getElementById("skip_all_popup");

export function BuildPopUp(messageText, confirmText, onConfirm, cancelText, onCancel) {
	if (popUpsDisabled())
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


export function BuildManyButtonPopUp(text, buttons) {
    if (!Array.isArray(buttons) || buttons.length === 0) {
        throw new Error(
            'BuildPopUp expects: BuildPopUp("message", [{ text: "OK", onClick: () => {} }])'
        );
    }

    if (popUpsDisabled()) {
        buttons[0].onClick?.(); // the method of the first button will run if nothing is selected?
        return;
    }

    const dialog = document.createElement("dialog");
    const message = document.createElement("p");
    const form = document.createElement("form");

    message.textContent = text;
    form.method = "dialog"; 

    for (let i = 0; i < buttons.length; i++) {
        const button = document.createElement("button");
        button.textContent = buttons[i].text;
        button.addEventListener("click", buttons[i].onClick);
        button.value = buttons[i].text;
        form.append(button);
    }

    dialog.append(message, form);

    dialog.addEventListener("close", () => {
        if(dialog.returnValue !== "")  console.log(`${dialog.returnValue} button clicked`);
        else console.log("popup closed");
        dialog.remove();
    });

    document.body.appendChild(dialog);
    dialog.showModal();
}






