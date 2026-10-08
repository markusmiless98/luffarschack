const CUBE_AMOUNT = 4;   // each layer is CUBE_AMOUNT * CUBE_AMOUNT amount of cubes
const LAYERS = 4;
const CUBE_SIZE = 10
const CUBE_GAP = 1.2;    // spacing between cubes 
const GAP_MIN = 13;      // min gap between layers for slider
const GAP_MAX = 60;      // max gap between layers for slider
const NS = 'http://www.w3.org/2000/svg'; //need this for svg elements

let layerGap = 40;       //starting gap between layers

const svg = document.querySelector('.svg');
const controls = document.querySelector('.controls');
const layers = [];
const buttons = [];

function cubinator(cubeSize) {
    //points
    let topmost = "0,0"
    let leftmostup = `${-cubeSize},${cubeSize / 2}`
    let rightmostup = `${cubeSize},${cubeSize / 2}`
    let leftmostdown = `${-cubeSize},${cubeSize + cubeSize / 2}`
    let rightmostdown = `${cubeSize},${cubeSize + cubeSize / 2}`
    let downmost = `0,${cubeSize * 2}`
    let center = `0,${cubeSize}`

    //here i build the svg for the individual cubes
    return `
            <polygon class="hit" points="${leftmostup} ${topmost} ${rightmostup} ${rightmostdown} ${downmost} ${leftmostdown}"/>
            <g class="cube">
                <polygon class="topFace"   points="${topmost} ${rightmostup} ${center} ${leftmostup}"/>
                <polygon class="leftFace"  points="${leftmostup} ${center} ${downmost} ${leftmostdown}"/>
                <polygon class="rightFace" points="${center} ${rightmostup} ${rightmostdown} ${downmost}"/>
            </g>`;
}

// Build the layers. Layer 0 is the bottom one, and each one after it is drawn later so it might be technically upside down? z value inverted... dunno how to fix that
function layerinator() {
    for (let l = 0; l < LAYERS; l++) {
        const layer = document.createElementNS(NS, 'g'); //i Aparently need to use createElementNS to create an svg element
        layer.setAttribute('class', 'layer');

        for (let x = 0; x < CUBE_AMOUNT; x++) {
            for (let y = 0; y < CUBE_AMOUNT; y++) {
                const offsetX = (x - y) * CUBE_SIZE * CUBE_GAP;
                const offsetY = (x + y) * CUBE_SIZE / 2 * CUBE_GAP;

                const cell = document.createElementNS(NS, 'g');
                cell.setAttribute('class', 'cell');
                cell.style.transform = `translate(${offsetX}px, ${offsetY}px)`;//translate = move element by offsetX pixels right and offsetY pixels down

                cell.innerHTML = cubinator(CUBE_SIZE) //set the svg contents like this

                cell.addEventListener("click", () => {
                    console.log(`this cube be x:${x} y:${y} z:${l}`)
                    cell.classList.toggle('selected'); //can make diffrent css classes for 
                });

                layer.appendChild(cell);
            }
        }
        svg.appendChild(layer);
        layers.push(layer);
    }
}
layerinator()

const box = svg.getBBox();//is like get the values of the box round the svg after elements created, ill be honest i dont fully understand how dis work but i need it otherwize it wont work
const extra = (LAYERS - 1) * layerGap;
svg.setAttribute("viewBox",`${box.x} ${box.y} ${box.width} ${box.height}`);

// Select one layer or pass null to show all of them.
function select(layerIndex) {
    //the svg contains the layers and the layers have the .Layer class and that class has a .active 
    svg.classList.toggle('focus', layerIndex !== null); //if layer index is null then no focus thus all layer visible

    for (let i = 0; i < layers.length; i++) {
    layers[i].classList.toggle('active', i === layerIndex)//if index is LayerIndex then activate layer
    }

    for (let i = 0; i < layers.length; i++) {
       buttons[i].classList.toggle('active', i === (layerIndex === null ? 0 : layerIndex + 1)) //nts:button on index 0 is all layers buttons after have index 123 
    }
}

function addLayerButtons() {
    const labels = ['All'];//one value for the all button

    for (let i = 0; i < layers.length; i++) {
            labels.push(`Layer ${i + 1}`);//add button lable for every layer
    }

    for (let i = 0; i < labels.length; i++) {
        const button = document.createElement('button');
        button.textContent = labels[i];

        button.addEventListener('click', () => {
            if (i === 0) {
                select(null);
            } else {
                select(i - 1);//buttons have extra index becuse all button
            }
        });
        controls.appendChild(button);
        buttons.push(button);
    }
}
addLayerButtons()

// Position every layer according to the current gap.
function changeLayerGap() {
    for (let i = 0; i < layers.length; i++) {
    layers[i].style.transform = `translate(0px, ${-i * layerGap}px)`; //nts: -i means move each layer push uppwards if i = positive then layers move down
    }
}

const gapInput = document.querySelector('#gapSlider');
const gapValue = document.querySelector('#gapValue');

gapInput.min = GAP_MIN;
gapInput.max = GAP_MAX;
gapInput.value = layerGap;
gapValue.textContent = layerGap;

gapInput.addEventListener('input', () => {
    layerGap = Number(gapInput.value);
    gapValue.textContent = layerGap;
    changeLayerGap();
});


// Clicking empty space, or pressing Escape, goes back to showing everything.
svg.addEventListener('click', () => select(null));
document.addEventListener('keydown', (e) => { if (e.key === 'Escape') select(null); });


changeLayerGap();
select(null);
