function buildGameViewSvg(onCubeClick = (x,y,z) => console.log(`this cube was x:${x} y:${y} z:${z}`)) {
    const SVG = document.querySelector('.svg');
    const LAYERS = [];
    const BUTTONS = [];


    const GAP_MAX = 60;

    layerinator(SVG, LAYERS, onCubeClick());
    positionLayers(LAYERS, 40)//sets start gap between layers
    setSvgViewBoxSize(SVG);
    buildLayerFocusToggels(BUTTONS, LAYERS, SVG)
    select(null, LAYERS, BUTTONS, SVG)
}

// === svg functions ===
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

function layerinator(
    //these are the only params you should need to create the image
    svg, 
    layers = [], 
    onCubeClick = (x, y, z) => console.log(`this cube is x:${x} y:${y} z:${z}`),

    //i set some generic settings here can be changed of overidden by sending in diffrent params
    cubeSize = 10, 
    cubeAmount = 4, 
    cubeGap = 1.2, 
    layerAmount = 4, 
    ns = 'http://www.w3.org/2000/svg', 
    ) {
    for (let l = 0; l < layerAmount; l++) {
        const layer = document.createElementNS(ns, 'g'); //i Aparently need to use createElementNS to create an svg element
        layer.setAttribute('class', 'layer');

        for (let x = 0; x < cubeAmount; x++) {
            for (let y = 0; y < cubeAmount; y++) {
                const offsetX = (x - y) * cubeSize * cubeGap;
                const offsetY = (x + y) * cubeSize / 2 * cubeGap;

                const cell = document.createElementNS(ns, 'g');
                cell.setAttribute('class', 'cell');
                cell.style.transform = `translate(${offsetX}px, ${offsetY}px)`;//translate = move element by offsetX pixels right and offsetY pixels down

                cell.innerHTML = cubinator(cubeSize) //set the svg contents like this

                cell.addEventListener("click", () => {
                    //console.log(`this cube be x:${x} y:${y} z:${l}`)
                    cell.classList.toggle('selected'); //can make diffrent css classes for
                    onCubeClick(x, y, l);
                    

                });

                layer.appendChild(cell);
            }
        }
        svg.appendChild(layer);
        layers.push(layer);
    }
}

function setSvgViewBoxSize(svg) {
    const box = svg.getBBox();//is like get the values of the box round the svg after elements created, ill be honest i dont fully understand how dis work but i need it otherwize it wont work
    svg.setAttribute("viewBox", `${box.x} ${box.y} ${box.width} ${box.height}`);
}

function positionLayers(layers, gap) {
    for (let i = 0; i < layers.length; i++) {
        layers[i].style.transform = `translate(0px, ${-i * gap}px)`;
    }
}


buildGameViewSvg()

//=== button functions ===


function buildLayerFocusToggels(buttons, layers, svg) {
    const labels = ['All'];
    const layerToggles = document.getElementById("layer_toggles")

    for (let i = 0; i < layers.length; i++) {
        labels.push(`Layer ${i + 1}`);
    }

    for (let i = 0; i < labels.length; i++) {
        const button = document.createElement('button');
        button.textContent = labels[i];

        button.addEventListener('click', () => {
            if (i === 0) {
                select(null, layers, buttons, svg);
            } else {
                select(i - 1, layers, buttons, svg);
            }
        });
        layerToggles.appendChild(button);
        buttons.push(button);
    }
}

function select(layerIndex, layers, buttons, svg) {
    //the svg contains the layers and the layers have the .layer class and that class has .active 
    svg.classList.toggle('focus', layerIndex !== null); //if layer index is null then true toggels off layer.active

    for (let i = 0; i < layers.length; i++) {
        layers[i].classList.toggle('active', i === layerIndex)//if index is LayerIndex then toggle layer on
    }

    for (let i = 0; i < layers.length; i++) {
        buttons[i].classList.toggle('active', i === (layerIndex === null ? 0 : layerIndex + 1)) //ternary for selecting the all button on index 0 if value is null
    }
}