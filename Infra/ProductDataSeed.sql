
use productapidb

-- select * from cat.Brands
-- SELECT * from cat.Categories
-- select * from cat.CatalogItems
-- select * from cat.ProductTags
-- select * from cat.ProductVariants
-- select * from cat.ProductImages
-- select * from cat.ProductReviews
-- select * from UserApiDb.dbo.AspNetUsers



INSERT INTO cat.Brands
    (Name, Description)
VALUES
    ('LuminaTech', 'Specializing in circadian-aligned smart lighting and ocular-health focused hardware.'),
    ('EcoVerve', 'Sustainable outdoor gear engineered from recycled ocean plastics and organic polymers.'),
    ('AeroPace', 'High-performance ergonomic office solutions designed for long-duration cognitive tasks.'),
    ('ZenithAudio', 'Audiophile-grade sound equipment focusing on spatial transparency and lossless wireless transmission.'),
    ('VitaGlow', 'Bio-adaptive skincare utilizing peptide chains for dermatological restoration.'),
    ('TerraTrek', 'Ruggedized exploration equipment for extreme thermal environments.'),
    ('NovaKitchen', 'Precision culinary instruments with integrated IoT for molecular gastronomy.'),
    ('PureFlow', 'Advanced water and air filtration systems using multi-stage HEPA and carbon nanotechnology.'),
    ('OmniWear', 'Adaptive apparel featuring thermal-regulating fabric and biometric sensing threads.'),
    ('TitanTools', 'Industrial-grade power tools with haptic feedback and brushless motor efficiency.');

INSERT INTO cat.Categories
    (Name, Description)
VALUES
    ('Smart Home', 'Connected devices designed to automate environmental variables and enhance residential efficiency.'),
    ('Outdoor & Adventure', 'Equipment and apparel designed for durability in unconditioned environments and wilderness navigation.'),
    ('Health & Wellness', 'Personal care and biological monitoring tools aimed at optimizing physical and mental states.'),
    ('Professional Computing', 'Hardware optimized for high-throughput workflows, ergonomic comfort, and digital creativity.'),
    ('Culinary Tech', 'Modern kitchen appliances that utilize sensor data to achieve consistent gastronomic results.');

INSERT INTO cat.CatalogItems
    (Slug, BaseName, IsPublished, CategoryId, BrandId, ShortSummary, SemanticDescription)
VALUES
    -- BRAND 1 (743c...)
    ('quantum-sync-router', 'QuantumSync Mesh Router X1', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A high-performance networking hub designed to eliminate dead zones in multi-story residential buildings. It utilizes AI-driven beamforming technology to prioritize bandwidth for low-latency gaming and high-definition video conferencing simultaneously.', 'Tri-band Wi-Fi 7 wireless access point. PHY rates up to 19Gbps. Incorporates 6GHz spectrum, 320MHz channel width, and 4K-QAM modulation. Quad-core 2.2GHz ARM processor.'),
    ('neural-link-headset', 'NeuralLink Cognitive Headset', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'An advanced wearable device that monitors neural oscillations to provide real-time feedback on focus levels. Ideal for knowledge workers who need to optimize deep-work sessions and identify peak alertness windows.', 'Non-invasive EEG biosensor array. 16 dry-electrode channels for prefrontal cortex monitoring. Data via BLE 5.2. Integrated neurofeedback algorithms for Alpha/Beta wave modulation.'),
    ('ion-drive-fan', 'IonDrive Bladeless Purifier', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A silent, bladeless air circulation system that doubles as a medical-grade purifier. It uses ionic propulsion to move air, making it significantly quieter than traditional fans while capturing ultra-fine dust.', 'Bladeless air multiplier with H13 HEPA filtration. Airflow capacity 450 CFM. CADR rating 350m3/h. Features PM2.5 and VOC air quality sensors with real-time OLED telemetry.'),
    ('spectre-laptop-v9', 'Spectre Pro Workstation 16', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A mobile powerhouse for 3D rendering and large-scale data analysis. It features a vapor-chamber cooling system that prevents thermal throttling during high-load tasks, ensuring consistent performance for professional creators.', '16-inch Mini-LED display (120Hz, 1000 nits). Intel Core i9-14900HX equivalent architecture. 64GB DDR5 RAM. Vapor chamber thermal management. Thunderbolt 5.0 I/O integration.'),
    ('core-watch-ultra', 'CoreSync Fitness Watch', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'The definitive wearable for metabolic tracking, offering hydration alerts and recovery scores based on heart rate variability. It helps athletes avoid overtraining by analyzing sleep architecture and daily exertion levels.', 'Sapphire crystal display. ECG/EKG monitoring. SpO2 and skin temperature sensors. 10ATM water resistance. Dual-band GNSS for precision mapping. 14-day battery cycle.'),
    ('optic-stream-cam', 'OpticStream 4K Conference Cam', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A smart webcam with auto-framing capabilities that follows the speaker around the room. It ensures professional video quality for remote presenters by automatically adjusting exposure based on ambient lighting conditions.', '4K Sony STARVIS sensor. AI-powered auto-framing and speaker tracking. Integrated beamforming microphone array with noise suppression. USB-C 3.2 plug-and-play architecture.'),
    ('data-vault-ssd', 'DataVault Biometric SSD 4TB', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'An ultra-secure external storage drive that requires fingerprint authentication for access. It is designed for traveling professionals who need to protect sensitive client data against physical theft or unauthorized digital intrusion.', 'NVMe PCIe Gen 4 internal architecture. AES 256-bit hardware encryption. Integrated capacitive fingerprint sensor. IP65 rated for water and dust resistance. Read speeds up to 2000MB/s.'),
    ('pulse-soundbar', 'Pulse Cinema Soundbar V3', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A compact yet powerful soundbar that utilizes wall-reflection technology to create a 7.1.4 Dolby Atmos experience without the need for rear speakers. It calibrates itself to your room dimensions using an internal microphone.', '9-driver array with upward-firing height channels. HDMI eARC support. Calibration via acoustic room-mapping algorithm. Supports AirPlay 2 and Spotify Connect. Wireless 8-inch subwoofer included.'),
    ('aura-pad-pro', 'AuraPad Wireless Charger', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'A multi-device charging station that detects the optimal wattage for any smartphone, earbud case, or smartwatch placed on its surface. Its cooling fan ensures that devices remain at a safe temperature while fast-charging.', 'Triple-coil Qi-certified charging surface. 15W per device output. Integrated silent cooling fan. Foreign Object Detection (FOD) safety circuitry. LED status indicators with auto-dimming.'),
    ('nexus-tablet-12', 'Nexus 12 Digital Canvas', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '743c433e-f36b-1410-8adb-00ab2e36627d', 'Designed for digital illustrators, this tablet offers a paper-like friction surface and zero-latency stylus input. Its high color accuracy makes it the perfect tool for professional color grading and photo retouching on the go.', '12.9-inch OLED display. 100% DCI-P3 color gamut. Pressure-sensitive stylus with 8192 levels. 120Hz ProMotion tech. Lightweight magnesium alloy body with anti-glare etching.'),

    ('hydro-pure-system', 'HydroPure Nano-Filter', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A comprehensive under-sink water purification unit that employs nanotechnology to strip away microplastics and heavy metals while retaining essential minerals for optimal hydration in a sleek, easy-to-install chassis.', 'Graphene-oxide filtration membrane. Flow rate: 2.5 GPM. Integrated TDS sensors. Wi-Fi telemetry for filter life. NSF/ANSI 58 certified.'),
    ('solar-tile-apex', 'SolarTile Apex Shingle', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'An aesthetic alternative to traditional solar panels, these shingles integrate directly into the roofline. They provide high-efficiency energy harvesting without compromising architectural integrity.', 'Monocrystalline PV element. 22.4% conversion efficiency. Tempered glass with non-reflective coating. Max system voltage 1000V DC.'),
    ('eco-vert-bin', 'EcoVert Smart Composter', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A countertop device that turns kitchen scraps into nutrient-rich soil in less than 24 hours. It operates silently and uses carbon filtration to ensure your kitchen remains free of any composting odors.', 'Automated thermophilic decomposition chamber. Dual-filter odor neutralization. Energy consumption 0.8kWh per cycle. 3-liter internal capacity.'),
    ('bio-moss-purifier', 'BioMoss Air Wall', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A living air filter that uses specialized moss cultures to absorb CO2 and VOCs. It brings a touch of nature indoors while actively improving the oxygen levels and air quality of urban apartments.', 'Self-watering modular moss panel. Integrated humidity and light sensors. Micro-ventilation system for CO2 absorption. Automated nutrient dosing.'),
    ('green-grid-battery', 'GreenGrid Home Storage 10', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A high-capacity home battery that stores excess solar energy for use at night or during power outages. Its slim profile allows for easy mounting in garages or on exterior walls, ensuring energy independence.', 'Lithium Iron Phosphate (LiFePO4) chemistry. 10kWh usable capacity. Peak power 7kW. IP65 rated. Scalable up to 3 units in parallel.'),
    ('regen-pump-system', 'Regen Heat Pump Pro', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'An ultra-efficient heating and cooling solution that extracts thermal energy from the air. It significantly reduces residential carbon footprints compared to traditional gas furnaces or electric resistive heating.', 'Air-source heat pump with variable speed inverter. SEER2 rating of 22. Operating range down to -15F. Environmentally friendly R32 refrigerant.'),
    ('wind-nest-turbine', 'WindNest Micro-Turbine', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A vertical-axis wind turbine designed for urban rooftops. Unlike traditional blades, it is bird-safe and operates at very low noise levels, making it ideal for supplement energy generation in windy city corridors.', 'Helical vertical axis design. Startup wind speed 2.5m/s. Rated output 400W. Magnetic braking safety system. Low-vibration mounting bracket.'),
    ('ocean-poly-rug', 'EcoWave Recycled Rug', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A durable, weather-resistant rug made entirely from upcycled ocean plastics. Its soft texture belies its rugged construction, making it perfect for both high-traffic living rooms and outdoor patios.', '100% rPET (recycled polyethylene terephthalate) fiber. UV-stabilized dyes. Mold and mildew resistant weave. Hand-loomed sustainable construction.'),
    ('flux-smart-meter', 'Flux Energy Monitor', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A real-time energy monitor that installs in your breaker panel. It identifies exactly which appliances are wasting power, helping users reduce their monthly utility bills through actionable data and mobile alerts.', 'Current clamp sensor technology. Sampling rate 1MHz. Machine learning for appliance fingerprinting. Wi-Fi bridge included. Mobile dashboard for kWh tracking.'),
    ('terra-grow-pod', 'TerraGrow Indoor Farm', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '773c433e-f36b-1410-8adb-00ab2e36627d', 'A vertical hydroponic garden that allows anyone to grow fresh herbs and greens year-round. Its automated lighting and nutrient delivery system ensure a harvest even for those with no gardening experience.', 'Vertical aeroponic tower. Full-spectrum LED array (200W). Automated pH and nutrient monitoring. Water-recirculation system reduces usage by 90%.'),

    ('titan-impact-drill', 'Titan Impact Drill G3', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A professional-grade power tool featuring a high-torque brushless motor for extreme durability in masonry and metalwork. The ergonomic grip reduces vibration by 40% for long-duration use.', '20V brushless motor. 2500 in-lbs torque. Variable speed trigger. Haptic kickback protection. Magnesium gear housing.'),
    ('forge-steel-saw', 'ForgeSteel Table Saw Pro', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A high-precision table saw with an integrated skin-sensing safety system that stops the blade instantly upon contact. Its cast-iron table ensures a perfectly flat surface for accurate woodworking cuts.', '15-Amp motor (4800 RPM). 10-inch carbide-tipped blade. Electronic blade brake. Rack and pinion fence system. Cast iron worktop for stability.'),
    ('laser-level-x5', 'LaserLevel 360 Degree Cross', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'Projects high-visibility green laser lines in all directions, making it easy to align cabinets, flooring, and structural framing across large open rooms with sub-millimeter precision.', 'Self-leveling green beam laser. Accuracy +/- 1/16in at 30ft. 150ft visible range. IP54 water resistance. Magnetic pivoting base.'),
    ('arc-welder-nano', 'ArcWelder Portable Inverter', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A lightweight, portable welding unit that provides professional-quality arcs from a standard household outlet. Its digital interface allows for precise current control, making it ideal for both hobbyists and field repairs.', 'IGBT inverter technology. 140A output at 60% duty cycle. Hot start and arc force control. Dual voltage input (110V/220V). Weighs only 12 lbs.'),
    ('torque-wrench-digital', 'Digital Torque Wrench X-Spec', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'Ensures every bolt is tightened to exact manufacturer specifications with audible and haptic alerts. Perfect for high-stakes mechanical work on carbon fiber bicycles or automotive engine components.', 'Range: 5-100 ft-lbs. Accuracy +/- 2%. LCD display with peak-hold. Chrome vanadium construction. Selectable units: Nm, ft-lb, in-lb, kg-cm.'),
    ('heavy-lift-jack', 'HeavyLift Hydraulic Jack 4T', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A low-profile floor jack capable of lifting heavy SUVs and trucks with ease. Its dual-piston rapid pump system reaches full height in half the time of standard jacks while providing a stable, wide base.', '4-ton lifting capacity. Low profile (3.5 inch clearance). Dual-piston hydraulic pump. Overload safety valve. Reinforced steel chassis.'),
    ('multi-tool-prime', 'MultiTool Prime 24-in-1', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'The ultimate everyday carry companion, featuring aerospace-grade stainless steel tools that fold into a compact, pocket-friendly handle. It replaces a full toolbox for quick household fixes and emergency field repairs.', 'S30V steel blade. Titanium handle scales. Spring-action pliers. Replaceable wire cutters. Magnetic bit driver with 10-piece bit set.'),
    ('vac-pro-shop', 'VacPro Industrial Shop Vac', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A heavy-duty wet/dry vacuum designed for construction sites. It features a self-cleaning filter system that prevents suction loss when vacuuming fine drywall dust or large debris from workshop floors.', '6.5 Peak HP motor. 12-gallon stainless tank. Automatic filter pulse cleaning. Integrated blower port. 20ft crush-resistant hose.'),
    ('grinder-max-angle', 'GrinderMax Brushless 4.5"', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A cordless angle grinder that delivers the power of a corded tool without the trip hazard. Its electronic brake stops the wheel in under two seconds, making it one of the safest grinders on the market.', '9000 RPM brushless motor. Tool-free guard adjustment. Paddle switch with lock-off. E-Clutch system for pinch protection. 18V/20V battery platform.'),
    ('lathe-mini-work', 'PrecisionLathe Mini Desktop', 1, '933c433e-f36b-1410-8adb-00ab2e36627d', '7a3c433e-f36b-1410-8adb-00ab2e36627d', 'A compact metal-turning lathe for model makers and small-parts prototyping. Despite its size, it maintains rigid tolerances for threading and turning soft metals like aluminum, brass, and copper.', '7x14 inch work envelope. Variable speed (50-2500 RPM). Digital tachometer. All-metal gears. 3-jaw chuck and tailstock included.'),

    ('sous-vide-pro', 'OmniChef Sous-Vide Stick', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Transform any pot into a precision cooking vessel. This immersion circulator keeps water within 0.1 degrees of accuracy, ensuring your proteins are cooked perfectly from edge to edge every single time.', '1200W heating element. IPX7 waterproof. Brushless DC motor. Temp range 20C-95C. Wi-Fi IoT connectivity for remote cook tracking.'),
    ('smart-air-fryer', 'OmniFry Digital Air Station', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Cook crispy, fried-style meals with 85% less oil using high-velocity convection. Its large capacity and dual-basket design allow you to cook an entire main course and side dish simultaneously without flavor transfer.', '6-quart dual basket. 1700W convection system. 12 one-touch cooking programs. Dishwasher safe non-stick coating. Temperature range 100F-450F.'),
    ('molecular-scale', 'Precision Molecular Scale', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Designed for modern pastry chefs and mixologists who need sub-gram accuracy for hydrocolloids and delicate flavorings. Its high-resolution display ensures you never ruin a recipe with imprecise measurements.', '0.01g resolution. 500g capacity. Stainless steel weighing platform. Tare function. Backlit LCD. Calibrated for high-precision culinary chemistry.'),
    ('infuse-smoker-gun', 'InfusePro Cold Smoke Gun', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Add a rich smoky aroma to meats, cheeses, or cocktails without applying heat. This handheld device uses wood chips to create a dense, flavorful smoke that can be directed into covered containers for a theatrical finish.', 'Handheld cold-smoke generator. Removable burn chamber. Two-speed fan control. Battery operated. Includes wood chip sample kit (Hickory/Applewood).'),
    ('blade-master-sharp', 'BladeMaster Electric Sharpener', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Restore a factory-fresh edge to your kitchen knives in seconds. This three-stage sharpening system uses diamond abrasives and flexible stropping disks to ensure every blade is razor-sharp and durable.', '3-stage sharpening (Coarse/Fine/Strop). 100% diamond abrasives. Precision angle guides (15 and 20 degree). Suitable for straight and serrated blades.'),
    ('froth-pro-mixer', 'FrothPro Steam Wand Mixer', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Create barista-quality microfoam for lattes and cappuccinos in your home kitchen. Its high-speed induction whisk creates thick, velvety foam from dairy and plant-based milks at the touch of a single button.', 'Induction heating frother. 4 settings (Hot Foam, Cold Foam, Hot Milk, Chocolate). 500ml capacity. Stainless steel jug. Automatic shut-off safety.'),
    ('espresso-logic-1', 'EspressoLogic Semi-Auto', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'A prosumer espresso machine with a built-in conical burr grinder. It features PID temperature control to ensure that every shot is extracted at the perfect temperature for optimal flavor profile and crema.', '15-bar Italian pump. PID temperature control. Integrated conical burr grinder with 30 settings. 58mm portafilter. Dedicated steam wand for latte art.'),
    ('pressure-chef-multi', 'PressureChef 8-in-1', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Reduce cooking times by 70% while locking in nutrients. This multi-cooker functions as a pressure cooker, slow cooker, rice cooker, and yogurt maker, making it the most versatile appliance on your counter.', '8-quart capacity. 1200W heating. 10 safety mechanisms. Stainless steel inner pot. Digital presets for meat, grains, and sautéing.'),
    ('vacuum-seal-elite', 'VacuumSeal Elite V2', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Extend the shelf life of your food by five times by removing air and sealing in freshness. This system is essential for sous-vide cooking and for preventing freezer burn on bulk meat purchases.', 'Heavy-duty vacuum pump. Built-in bag cutter and roll storage. Pulse function for delicate foods. Dry/Moist modes. External hose for canister sealing.'),
    ('smart-tea-steep', 'TeaSteep Precision Kettle', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '7d3c433e-f36b-1410-8adb-00ab2e36627d', 'Brew any type of tea at its specific ideal temperature. This gooseneck kettle offers degree-by-degree control, allowing you to unlock the full flavor of delicate green teas without the bitterness of boiling water.', '1.0L capacity. Gooseneck spout for precision pour. 1200W rapid boil. 60-minute hold temp function. Real-time temperature LCD display.'),

    ('vitascan-monitor', 'VitaScan Non-Invasive Tracker', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A revolutionary wearable that tracks metabolic health through dermal sensors. It provides immediate feedback on how different foods affect your energy levels, helping users optimize their diet via data.', 'Optical spectroscopy sensor. Measures interstitial glucose trends. Sample rate 5min. Syncs with mobile dashboards. Medical-grade silicone build.'),
    ('zen-breath-pacer', 'ZenBreath Respiratory Trainer', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A handheld device that guides you through breathing exercises to lower stress and improve lung capacity. It uses haptic pulses to signal inhalation and exhalation, making mindfulness accessible anywhere.', 'Biofeedback respiratory sensor. Haptic vibration motor. Bluetooth sync for stress-tracking app. USB-C rechargeable. Pocket-sized ergonomic design.'),
    ('sleep-halo-ring', 'SleepHalo Bio-Ring', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A discreet titanium ring that monitors your sleep stages and body temperature. It provides a daily readiness score to help you decide whether to push your physical limits or focus on recovery.', 'Grade 5 Titanium shell. Infrared PPG sensor. Accelerometer for motion tracking. NTC temperature sensor. 7-day data storage without sync.'),
    ('luma-light-mask', 'LumaGlow Red Light Shield', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A professional-grade LED therapy mask that uses specific wavelengths of red and near-infrared light to stimulate collagen production and reduce skin inflammation in just ten minutes a day.', 'Red (630nm) and NIR (850nm) LEDs. medical-grade silicone. 10-minute auto-timer. Rechargeable controller. Eye-protection inserts included.'),
    ('flex-foam-roller', 'FlexPulse Vibrating Roller', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'Combine traditional foam rolling with high-intensity vibration to penetrate deeper into muscle tissue. It speeds up recovery and reduces muscle soreness after intense athletic training or long days on your feet.', 'High-density EPP foam. 4 vibration intensity levels. Internal rechargeable battery (2h life). Lightweight design for travel. Carrying bag included.'),
    ('posture-pod-wear', 'PosturePod Smart Sensor', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A tiny sensor that clips to your clothing and vibrates whenever you slouch. It trains your body to sit and stand straighter, effectively reducing chronic neck and back pain through gentle habit reinforcement.', 'MEMS accelerometer. Precise tilt sensing. Hypoallergenic adhesive or magnetic clip. Daily posture score via app. 10-day battery life.'),
    ('hydro-flask-smart', 'VitaHydrate Smart Bottle', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A vacuum-insulated water bottle that tracks your daily water intake and glows to remind you to drink. It syncs with your fitness apps to adjust your hydration goals based on your daily activity levels.', 'Stainless steel vacuum insulation. Flow-meter lid. RGB LED reminder ring. Bluetooth app integration. 24-ounce capacity. BPA-free.'),
    ('keto-breath-meter', 'VitaKeto Metabolic Breathalyzer', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'Measure your ketone levels through your breath for a painless way to track nutritional ketosis. It provides an instant reading of your fat-burning state, helping you stay on track with your dietary goals.', 'Acetone gas sensor. High-sensitivity semiconductor. LCD result display. Replaceable mouthpieces. Syncs with ketogenic tracking software.'),
    ('deep-tissue-gun', 'VitaRelief Percussion Gun', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'A quiet, high-torque massage gun that delivers rapid bursts of pressure into tight muscles. It improves blood flow and breaks up knots, making it an essential tool for recovery and pain management.', 'Brushless 24V motor. 12mm stroke length. 5 speed settings (1800-3200 PPM). 6 interchangeable massage heads. QuietForce technology noise dampening.'),
    ('uv-pure-wand', 'VitaShield UV-C Sanitizer', 1, '923c433e-f36b-1410-8adb-00ab2e36627d', '803c433e-f36b-1410-8adb-00ab2e36627d', 'Kill 99.9% of germs and bacteria on any surface with this portable UV-C light wand. It is perfect for sanitizing travel environments, hotel rooms, and high-touch electronics without the use of harsh chemicals.', 'UV-C LED technology (270nm). Safety gravity sensor (auto-off if tilted). Foldable design for portability. Rechargeable lithium battery. Lab-tested sterilization rates.'),

    ('terra-tread-boot', 'TerraTread Hiker Pro', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'The ultimate footwear for long-distance backpacking. These boots feature a dual-density midsole for maximum shock absorption and a waterproof membrane that keeps feet dry during river crossings.', 'Vibram Megagrip outsole. Gore-Tex ePE membrane. 900D Cordura upper. TPU stability shank. Anatomically shaped footbed.'),
    ('summit-shell-jax', 'SummitShell Alpine Parka', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'A lightweight but rugged hardshell jacket designed for mountaineering. It is fully windproof and waterproof, yet breathable enough to prevent moisture buildup during strenuous high-altitude climbs.', '3-layer hardshell construction. 20,000mm waterproof rating. Helmet-compatible hood. Pit-zips for ventilation. Fully taped seams.'),
    ('apex-pack-45', 'Apex Adventure Pack 45L', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'An ergonomically designed backpack that shifts weight to your hips to reduce shoulder strain. Its modular attachment points and rainproof cover make it the ideal choice for multi-day wilderness expeditions.', '45-liter capacity. Anti-gravity suspension system. Integrated hydration sleeve. 210D Ripstop Nylon. Adjustable torso length.'),
    ('trail-lite-tent', 'TrailLite 2-Person Ultra', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'A sub-two-pound tent that doesn''t sacrifice space. Its aerodynamic design and high-strength aluminum poles ensure stability in high winds, providing a reliable shelter for lightweight backpackers.', 'Weight: 1.8 lbs. Semi-freestanding design. DAC Featherlite poles. 1200mm silicone-coated nylon fly. Dual vestibules for gear storage.'),
    ('trek-torch-max', 'TrekTorch 1000 Headlamp', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'Illuminate your path with a powerful 1000-lumen beam. This headlamp features reactive lighting technology that automatically adjusts brightness based on where you are looking, preserving battery life for long hikes.', '1000 lumens peak. Reactive lighting sensor. IP67 waterproof. Red light mode for night vision. Rechargeable 3500mAh battery.'),
    ('zero-degree-bag', 'ZeroDegree Down Sleeper', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'Stay warm even in sub-freezing temperatures with this 800-fill power down sleeping bag. Its water-resistant down and mummy shape maximize heat retention, making it perfect for winter camping or high-altitude treks.', '800-fill hydrophobic down. 0F / -18C temperature rating. 20D Pertex Quantum shell. Draft collar and integrated hood. Compression sack included.'),
    ('path-finder-gps', 'PathFinder Satellite Link', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'A handheld satellite communicator that allows for two-way messaging and SOS alerts in areas without cell service. It is an essential safety device for solo adventurers venturing into remote wilderness.', 'Iridium satellite network. 100% global coverage. Two-way text messaging. SOS emergency trigger. Map and waypoint tracking.'),
    ('river-run-kayak', 'RiverRun Inflatable Kayak', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'A high-performance inflatable kayak that fits into a backpack. Its drop-stitch construction makes it as rigid as a hard-shell boat, allowing for stable paddling in both calm lakes and moderate whitewater.', 'Drop-stitch PVC construction. 10ft length. Weight capacity 300 lbs. Includes high-pressure pump and breakdown paddle. Self-bailing valves.'),
    ('camp-stove-jet', 'CampStove Jet Boil System', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'Boil water in under 100 seconds with this integrated stove and pot system. Its wind-blocking design and heat-exchange bottom make it the most fuel-efficient choice for backpacking and outdoor cooking.', 'FluxRing heat exchanger. 1-liter insulated pot. Piezo ignition. Neoprene cozy with heat indicator. Compatible with isobutane-propane canisters.'),
    ('filter-straw-max', 'FilterStraw Personal Purifier', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '833c433e-f36b-1410-8adb-00ab2e36627d', 'Drink safely from any stream or lake. This portable filter removes 99.999% of bacteria and protozoa, providing clean drinking water without the need for chemicals or pumping.', 'Hollow fiber membrane filter. 0.1 micron pore size. Filters up to 1000 gallons. Chemical-free purification. BPA-free construction.'),

    ('aero-desk-pro', 'AeroDesk Electric Stand', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'An enterprise-grade standing desk designed to promote movement. Its quiet dual-motor system transitions smoothly from sitting to standing, encouraging better posture during long office workdays.', 'Dual-motor lift. 350lb capacity. Height 24.5-50in. Anti-collision sensor. Bamboo desktop. OLED control panel.'),
    ('orbit-monitor-arm', 'Orbit Dual Monitor Mount', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'Free up desk space and improve ergonomics with this gas-spring monitor arm. It allows for effortless adjustment of your screens to the perfect height and angle, reducing eye and neck strain.', 'Gas-spring counterbalanced arms. Supports dual 32-inch monitors. VESA 75/100 compatible. Integrated cable management. Desk-clamp or grommet mount.'),
    ('zenith-focus-hub', 'Zenith USB-C Docking 12', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'A single-cable solution for your entire workstation. This docking station supports triple 4K displays and provides high-speed data transfer and power delivery for even the most demanding laptop setups.', '12-port USB-C dock. Triple 4K HDMI/DP support. 100W Power Delivery. Gigabit Ethernet. SD/MicroSD card reader. 10Gbps USB-A ports.'),
    ('cloud-key-mech', 'CloudKey Mechanical Keyboard', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'A silent mechanical keyboard optimized for open-plan offices. It provides the tactile feel of mechanical switches without the loud clicking, ensuring a comfortable typing experience for you and your colleagues.', 'Quiet tactile switches (Brown type). Hot-swappable PCB. Double-shot PBT keycaps. Wireless 2.4GHz and Bluetooth. RGB backlighting.'),
    ('glide-mouse-pro', 'GlideMaster Ergonomic Mouse', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'A vertical mouse designed to place your hand in a natural "handshake" position. It significantly reduces muscle strain and pressure on your carpal tunnel, making it perfect for users with RSI concerns.', '57-degree vertical tilt. 4000 DPI high-precision sensor. Customizable thumb buttons. USB-C rechargeable. Dual-connectivity (Dongle/BT).'),
    ('nexus-whiteboard', 'Nexus Digital Whiteboard', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'A wall-mounted E-Ink whiteboard that allows for real-time collaboration between remote teams. Anything written on the board is instantly digitized and shared to your team''s project management software.', '42-inch E-Ink display. Pressure-sensitive stylus. Wi-Fi cloud syncing. Low-power consumption (6-month battery). API integration for Jira/Slack.'),
    ('acoustic-panel-set', 'AeroSilence Acoustic Panels', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'Improve the audio quality of your home office or recording studio. These high-density foam panels absorb echo and reverberation, ensuring that your voice sounds clear and professional during video calls.', 'High-density polyester fiber. Noise Reduction Coefficient (NRC) 0.85. Beveled edge design. Flame retardant. Self-adhesive backing.'),
    ('privacy-screen-pro', 'AeroPrivacy Magnetic Filter', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'Keep sensitive information safe from prying eyes in public spaces. This magnetic screen protector blackens your display when viewed from the side, while maintaining perfect clarity for the user.', 'Microlouver technology. 60-degree viewing angle limit. Magnetic attachment. Blue light reduction. Matte/Glossy reversible finish.'),
    ('erg-foot-rest', 'AeroStep Adjustable Footrest', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'Support your lower back and improve circulation by keeping your legs at the correct angle. This adjustable footrest features a massaging surface that helps keep your feet active even while seated for long hours.', '3 height settings. 30-degree tilt range. Textured massaging surface. Non-slip rubber base. High-impact polystyrene construction.'),
    ('task-lamp-light', 'AeroLight Architect Desk Lamp', 1, '903c433e-f36b-1410-8adb-00ab2e36627d', '863c433e-f36b-1410-8adb-00ab2e36627d', 'The ultimate workspace lighting, featuring an ultra-wide beam that illuminates your entire desk without glare. Its smart sensors adjust brightness based on the time of day to help reduce digital eye strain.', 'Wide-angle LED head. Auto-dimming ambient sensor. CRI 95 for color accuracy. 5 color temperature settings. Clamp-mount space-saving design.'),

    ('lumina-glow-panel', 'LuminaGlow RGB Panel', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'A modular lighting system that doubles as wall art. Sync these panels with your music or gaming to create an immersive environmental atmosphere that changes color based on your digital content.', 'Modular LED smart system. 16.7M colors. Wi-Fi/Matter protocol support. Touch-sensitive panels. Up to 21 panels per controller.'),
    ('smart-blind-motor', 'LuminaShade Retrofit Motor', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Automate your existing window blinds with this easy-to-install motor. Schedule your blinds to open with the sunrise or close when you leave the house to improve energy efficiency and privacy.', 'Bluetooth/Zigbee motor. Rechargeable solar panel included. Fits standard bead chains. Voice control via Alexa/Google Home.'),
    ('entry-lock-face', 'LuminaSecure Facial Lock', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Never fumble for keys again. This smart lock uses advanced 3D facial recognition to unlock your door as you approach, while providing temporary access codes for guests or delivery personnel via an app.', '3D Infrared facial recognition. Anti-spoofing technology. Wi-Fi integrated. Emergency physical key backup. 1-year battery life.'),
    ('flood-sensor-pro', 'LuminaLeak Water Sensor', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Protect your home from costly water damage. This sensor detects leaks under sinks or near water heaters and sends an instant alert to your phone, allowing you to shut off the main water valve remotely.', 'Ultra-low profile sensor. Gold-plated detection pins. 5-year battery life. Wi-Fi connectivity. Loud 90dB local alarm.'),
    ('smart-vent-reg', 'LuminaVent Air Controller', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Optimize the temperature of every room individually. These smart vents open and close based on the occupancy and target temperature of specific rooms, reducing the load on your HVAC system.', 'Motorized air vent. Occupancy and temp sensors. Compatible with smart thermostats. Fits standard HVAC registers. Battery operated.'),
    ('bell-cam-ultra', 'LuminaBell Video Doorbell', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'See who is at your door from anywhere in the world. This doorbell features a wide-angle 2K camera with package detection and two-way audio, allowing you to give instructions to delivery drivers remotely.', '2K HDR video. 180-degree field of view. Package/Person/Vehicle detection. Dual-band Wi-Fi. Local storage option.'),
    ('switch-bot-link', 'LuminaBot Button Pusher', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Automate any existing non-smart appliance. This small mechanical arm can push buttons or toggle switches on coffee makers, light switches, or computers, bringing "dumb" devices into your smart home ecosystem.', 'Small mechanical actuator. Bluetooth control with Wi-Fi bridge option. Built-in timer. 3M adhesive mount. 600-day battery life.'),
    ('energy-curtain', 'LuminaDrape Smart Rod', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'Upgrade your bedroom with curtains that open automatically to wake you with natural light. This motorized rod supports heavy blackout curtains and integrates with your sleep tracking software for better mornings.', 'Telescoping motorized rod. Supports up to 25lbs. Light sensor for auto-open. Voice control compatible. USB-C rechargeable.'),
    ('thermo-stat-v2', 'LuminaStat Smart Controller', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'A learning thermostat that adapts to your schedule to save energy. It tracks outside weather and indoor humidity to ensure your home is always at the perfect temperature before you even walk through the door.', 'OLED touchscreen interface. Learning algorithms for schedule optimization. Geofencing support. Humidity and proximity sensors.'),
    ('smoke-detect-co', 'LuminaAir Smoke & CO Alarm', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '893c433e-f36b-1410-8adb-00ab2e36627d', 'A smart smoke detector that tells you exactly where the danger is located. It alerts your phone in case of fire or carbon monoxide, ensuring your family''s safety even when you aren''t home.', 'Split-spectrum smoke sensor. Electrochemical CO sensor. Voice alerts. Night light feature. Self-testing battery circuitry.'),


    ('pure-flow-tower', 'PureFlow HEPA Purifier', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'An advanced air filtration system using nanotechnology to capture viral particles and allergens. Its sleek tower design fits into any decor while providing clean air for rooms up to 1000 square feet.', '3-stage HEPA 13 filter. Carbon layer for odor removal. CADR 450. PM2.5 laser sensor. Wi-Fi connected for air quality history.'),
    ('tap-filter-lux', 'PureFlow Faucet System', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Get clean, great-tasting water directly from your tap. This faucet-mounted filter removes chlorine, lead, and sediment, and features a bypass switch for when you need unfiltered water for washing dishes.', 'Mineral-core filter technology. NSF 42/53 certified. Tool-free installation. 300-gallon filter life. Filter-change indicator LED.'),
    ('whole-home-filter', 'PureFlow Mainline System', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Protect your entire plumbing system and appliances. This whole-house filter removes sediment and chemicals from your main water line, ensuring every shower and faucet in your home delivers purified water.', 'High-flow 1-inch ports. 5-micron sediment pre-filter. Carbon block main stage. Pressure gauges included. 100,000 gallon capacity.'),
    ('shower-filter-pro', 'PureFlow Vitamin Shower', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Improve your skin and hair health by removing harsh chlorine from your shower water. This filter also infuses the water with Vitamin C and essential minerals, reducing dryness and irritation after bathing.', 'KDF-55 chlorine removal. Vitamin C infusion cartridge. High-pressure design. Chrome finish. Easy screw-on installation.'),
    ('car-air-purifier', 'PureFlow Auto Cabin Filter', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Protect yourself from traffic exhaust and urban pollution. This compact purifier fits in your car''s cup holder or clips to the headrest, cleaning the cabin air every few minutes while you drive.', 'H11 HEPA filter. Active carbon layer. Dual-fan system. USB-powered. Quiet operation <30dB.'),
    ('humid-clear-v3', 'PureFlow Smart Humidifier', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Maintain the perfect humidity level for respiratory health. This cool-mist humidifier features a UV-C light that kills bacteria in the water tank, ensuring the mist you breathe is clean and safe.', '4L tank capacity. 40-hour runtime. UV-C water sterilization. Built-in humidistat. Top-fill design. Essential oil tray.'),
    ('dry-air-dehumid', 'PureFlow Basement Dry 50', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Prevent mold and mildew in damp areas. This high-capacity dehumidifier removes up to 50 pints of moisture per day and features an internal pump to drain the water automatically through a window or floor drain.', '50 pints/day capacity. Internal condensate pump. Washable air filter. Energy Star certified. Low-temperature operation.'),
    ('portable-ac-lux', 'PureFlow Portable AC Unit', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Stay cool without permanent window installations. This portable air conditioner also filters the air and removes humidity, making it the perfect all-in-one solution for small apartments or server rooms.', '12,000 BTU cooling. Integrated HEPA filter. Dehumidifier mode. Remote control. Easy-install window kit included.'),
    ('ion-desktop-pure', 'PureFlow Desktop Ionizer', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'A personalized air purifier for your immediate workspace. It uses negative ion technology to drop dust and smoke particles out of the air, ensuring you breathe clean air even in busy office environments.', 'Negative ion generator. Ozone-free. USB-powered. Compact 6-inch footprint. Maintenance-free (no filters to change).'),
    ('alkaline-pitcher', 'PureFlow Mineral Pitcher', 1, '913c433e-f36b-1410-8adb-00ab2e36627d', '8c3c433e-f36b-1410-8adb-00ab2e36627d', 'Turn ordinary tap water into alkaline antioxidant water. This pitcher increases the pH of your water while removing contaminants, providing a refreshing and healthy way to stay hydrated throughout the day.', '10-cup capacity. 7-stage filter cartridge. Increases pH to 8.5-9.5. Reduces ORP (oxidation reduction potential). BPA-free.'),


    ('omni-wear-vest', 'OmniWear Thermal Vest', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'Adaptive apparel featuring thermal-regulating fabric and biometric sensing threads. This vest monitors your body temperature and activates heating zones to keep you comfortable in changing weather.', 'Graphene heating elements. Biometric thread sensors. 10,000mAh battery pack. Carbon fiber insulation. Water-resistant outer shell.'),
    ('flex-form-pants', 'OmniWear Motion Trousers', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'High-performance trousers designed for urban commuters. They feature hidden reflective details for night safety and a specialized fabric that resists stains and wrinkles while providing four-way stretch for cycling.', '4-way stretch Schoeller fabric. NanoSphere stain resistance. Articulated knees. Hidden zippered security pockets. Reflective cuff lining.'),
    ('aero-mesh-shirt', 'OmniWear CoolMesh Tee', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'A base layer that actively pulls heat away from your body. Perfect for high-intensity training or hot summer days, its antimicrobial treatment ensures it remains odor-free even after multiple workouts.', 'Xylitol-infused cooling fibers. Antimicrobial silver-ion finish. Flatlock seams for zero chafing. UPF 50+ sun protection.'),
    ('storm-tech-jacket', 'OmniWear StormGuard Pro', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'The ultimate urban rain jacket, combining high-fashion aesthetics with extreme weather protection. It is fully waterproof yet features a unique ventilation system that prevents the "sauna effect" during humid rainstorms.', '20k/20k waterproof-breathable membrane. Magnetic closures. Internal carry straps. Welded seams. Adjustable laser-cut vents.'),
    ('glide-run-shorts', 'OmniWear Sprint Shorts', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'Lightweight running shorts with a built-in compression liner that prevents chafing and provides muscle support. The rear zip pocket is perfectly sized for large smartphones and keeps them bounce-free during sprints.', 'Laser-perforated ventilation. Moisture-wicking liner. 5-inch inseam. 360-degree reflectivity. Bounce-free phone pocket.'),
    ('base-layer-warm', 'OmniWear Arctic Base', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'A Merino wool blend base layer that provides exceptional warmth without the bulk. It regulates your body temperature in cold environments and is naturally odor-resistant, making it ideal for multi-day ski trips.', '80% Merino Wool / 20% Recycled Polyester. 250gsm midweight. Offset shoulder seams for pack comfort. Thumb loops for layering.'),
    ('ultra-light-cap', 'OmniWear AeroCap', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'A packable running cap made from breathable, sweat-wicking fabric. Its flexible brim allows it to be folded into a pocket without losing its shape, while the dark under-brim reduces glare from the sun.', 'Crushable EVA foam brim. Laser-cut side panels. Moisture-wicking headband. Adjustable bungee closure. UPF 40 rating.'),
    ('tech-grip-gloves', 'OmniWear Haptic Gloves', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'Keep your hands warm without losing touch sensitivity. These gloves feature conductive fingertips for smartphone use and a silicone grip pattern on the palm to ensure your devices never slip from your hand.', 'Wind-stopper fleece. Conductive leather thumb and index. Silicone hexagon grip. Extended wrist cuff. Clip for keeping pairs together.'),
    ('recovery-sock-pro', 'OmniWear Compresso-Socks', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'Graduated compression socks that improve circulation and reduce swelling during long flights or after intense training sessions. Their targeted arch support and cushioned heel provide comfort for all-day wear.', '20-30 mmHg graduated compression. Padded Achilles protection. Left/Right specific fit. Breathable instep mesh. Antimicrobial yarn.'),
    ('omni-tote-bag', 'OmniWear Tech Tote', 1, '943c433e-f36b-1410-8adb-00ab2e36627d', '8f3c433e-f36b-1410-8adb-00ab2e36627d', 'A waterproof tote bag designed for the digital nomad. It features a padded laptop sleeve and multiple organization pockets for cables and chargers, making it easy to transition from the gym to the office.', 'X-Pac laminated fabric. YKK AquaGuard zippers. 16-inch laptop compartment. External water bottle pocket. Luggage pass-through strap.');

INSERT INTO cat.ProductVariants
    (CatalogItemId, Sku, Price, StockQuantity, AttributesJson)
VALUES
    -- a03d... (Electronics/Smart Home)
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A03D-BLK', 299.00, 45, '{"Color":"Obsidian Black","Connectivity":"Wi-Fi 7"}'),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A03D-WHT', 299.00, 32, '{"Color":"Arctic White","Connectivity":"Wi-Fi 7"}'),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A03D-SLV-PRO', 349.00, 12, '{"Color":"Brushed Silver","Connectivity":"Wi-Fi 7 Pro"}'),

    -- 883d... (Display Tech)
    ('883d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-883D-27', 450.00, 15, '{"ScreenSize":"27-inch","Panel":"OLED"}'),
    ('883d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-883D-32', 650.00, 8, '{"ScreenSize":"32-inch","Panel":"OLED"}'),

    -- 083e... (Adventure Gear)
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-083E-SM', 85.00, 100, '{"Size":"Small","Capacity":"15L"}'),
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-083E-MD', 95.00, 150, '{"Size":"Medium","Capacity":"25L"}'),
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-083E-LG', 110.00, 80, '{"Size":"Large","Capacity":"40L"}'),

    -- fc3d... (Health/Sensors)
    ('fc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-FC3D-STD', 199.00, 60, '{"Sensor":"Optical","Precision":"Standard"}'),

    -- 683d... (Apparel)
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-683D-S-NVY', 55.00, 40, '{"Size":"S","Color":"Navy"}'),
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-683D-M-NVY', 55.00, 65, '{"Size":"M","Color":"Navy"}'),
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-683D-L-NVY', 55.00, 30, '{"Size":"L","Color":"Navy"}'),
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-683D-XL-NVY', 55.00, 12, '{"Size":"XL","Color":"Navy"}'),

    -- f43c... (Consumables/Bio)
    ('f43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F43C-30P', 45.00, 500, '{"PackSize":"30 Capsules","Concentration":"500mg"}'),
    ('f43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F43C-90P', 110.00, 200, '{"PackSize":"90 Capsules","Concentration":"500mg"}'),

    -- b83c... (Industrial Tools)
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B83C-18V', 280.00, 20, '{"Voltage":"18V","MotorType":"Brushless"}'),
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B83C-24V', 350.00, 15, '{"Voltage":"24V","MotorType":"Brushless"}'),

    -- 143e... (Home Fragrance/Wellness)
    ('143e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-143E-LAV', 32.00, 100, '{"Scent":"Lavender","Duration":"40hrs"}'),
    ('143e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-143E-SAN', 32.00, 85, '{"Scent":"Sandalwood","Duration":"40hrs"}'),

    -- c43d... (Computing Accessory)
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C43D-GEN4', 120.00, 45, '{"Generation":"PCIe 4.0","Storage":"1TB"}'),
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C43D-GEN5', 210.00, 20, '{"Generation":"PCIe 5.0","Storage":"2TB"}'),

    -- cc3c... (Power Solutions)
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-CC3C-US', 89.00, 100, '{"PlugStandard":"US-TypeA","Output":"100W"}'),
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-CC3C-UK', 89.00, 50, '{"PlugStandard":"UK-TypeG","Output":"100W"}'),
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-CC3C-EU', 89.00, 75, '{"PlugStandard":"EU-TypeC","Output":"100W"}'),

    -- 203d... (Finishes/Materials)
    ('203d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-203D-GLS', 45.00, 200, '{"Finish":"Glossy","Volume":"1L"}'),
    ('203d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-203D-MAT', 45.00, 180, '{"Finish":"Matte","Volume":"1L"}'),

    -- 803d... (Wearables)
    ('803d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-803D-LT', 199.00, 40, '{"Weight":"Lightweight","Material":"Titanium"}'),

    -- e83d... (High-End Mobility)
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E83D-RED', 3200.00, 5, '{"Color":"Racing Red","Battery":"750Wh"}'),
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E83D-BLK', 3200.00, 3, '{"Color":"Matte Black","Battery":"750Wh"}'),

    -- 943d... (Water Tech)
    ('943d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-943D-STN', 75.00, 60, '{"FilterType":"Standard","StageCount":3}'),
    ('943d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-943D-ULT', 125.00, 25, '{"FilterType":"Ultrafiltration","StageCount":5}'),

    -- a83c... (Supplements)
    ('a83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A83C-BTL', 28.00, 300, '{"Container":"Glass Jar","Quantity":"60 Caps"}'),

    -- b03c... (Interface Hardware)
    ('b03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B03C-WRL', 150.00, 45, '{"Mode":"Wireless","Latency":"2ms"}'),
    ('b03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B03C-WRD', 120.00, 30, '{"Mode":"Wired","Latency":"0.5ms"}'),

    -- 583d... (Fitness Gear)
    ('583d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-583D-STD', 45.00, 500, '{"Thickness":"6mm","Surface":"Non-slip"}'),

    -- f03d... (Audio Hardware)
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F03D-OPEN', 599.00, 15, '{"Design":"Open-Back","Impedance":"300 Ohm"}'),
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F03D-CLOS', 599.00, 20, '{"Design":"Closed-Back","Impedance":"32 Ohm"}'),

    -- c83c... (Smart Lighting)
    ('c83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C83C-E26', 25.00, 400, '{"Socket":"E26","Brightness":"800lm"}'),
    ('c83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C83C-B22', 25.00, 200, '{"Socket":"B22","Brightness":"800lm"}'),

    -- cc3d... (Laptops)
    ('cc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-CC3D-I7', 1800.00, 10, '{"Processor":"Intel i7","RAM":"32GB"}'),
    ('cc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-CC3D-I9', 2400.00, 5, '{"Processor":"Intel i9","RAM":"64GB"}'),

    -- b83d... (Culinary Tech)
    ('b83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B83D-800W', 199.00, 40, '{"Power":"800W","Volume":"1.5L"}'),

    -- a83d... (Portable Lighting)
    ('a83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A83D-USB', 35.00, 300, '{"Charging":"USB-C","Lumens":"500"}'),

    -- 283d... (Photography)
    ('283d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-283D-BODY', 2200.00, 12, '{"Package":"Body Only","Mount":"Z-Mount"}'),
    ('283d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-283D-KIT', 2600.00, 8, '{"Package":"Lens Kit","Mount":"Z-Mount"}'),

    -- 843d... (Home Security)
    ('843d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-843D-INT', 145.00, 55, '{"Location":"Indoor","Resolution":"2K"}'),
    ('843d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-843D-EXT', 185.00, 40, '{"Location":"Outdoor","Resolution":"2K"}'),

    -- 483d... (Bio-Living)
    ('483d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-483D-UNIT', 12.00, 1000, '{"Consumable":"Filter Refill","Life":"30 Days"}'),

    -- 043e... (Smart Integration)
    ('043e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-043E-HUB', 120.00, 90, '{"Protocol":"Zigbee 3.0","Ethernet":"Yes"}'),

    -- bc3d... (Compression Apparel)
    ('bc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-BC3D-S', 40.00, 100, '{"Size":"S","Level":"High"}'),
    ('bc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-BC3D-M', 40.00, 150, '{"Size":"M","Level":"High"}'),
    ('bc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-BC3D-L', 40.00, 120, '{"Size":"L","Level":"High"}'),

    -- e03c... (Thermal Management)
    ('e03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E03C-SINGLE', 65.00, 80, '{"Zones":"Single","AppEnabled":"No"}'),
    ('e03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E03C-DUAL', 120.00, 45, '{"Zones":"Dual","AppEnabled":"Yes"}'),

    -- ec3c... (Hi-Fi Audio)
    ('ec3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-EC3C-DAC', 250.00, 25, '{"Chipset":"ESS Sabre","Interface":"USB/Opt"}'),

    -- 243d... (Networking)
    ('243d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-243D-SAT', 599.00, 10, '{"Antenna":"Active-Phased","Service":"Global"}'),

    -- 983d... (Technical Headwear)
    ('983d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-983D-OS', 25.00, 400, '{"Fit":"Adjustable","Reflective":"Yes"}'),

    -- 103e... (Lighting Design)
    ('103e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-103E-MOD', 210.00, 30, '{"Design":"Modular","PanelCount":9}'),

    -- d03c... (Kitchen Tools)
    ('d03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D03C-STN', 45.00, 200, '{"Material":"Steel","Grip":"Ergo"}'),

    -- 083d... (Renewable Energy)
    ('083d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-083D-400W', 850.00, 15, '{"PeakOutput":"400W","Type":"Mono"}'),

    -- fc3c... (Environmental Monitoring)
    ('fc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-FC3C-AIR', 115.00, 80, '{"Metric":"PM2.5/AQI","Battery":"Rechargeable"}'),

    -- ec3d... (Sustainable Consumables)
    ('ec3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-EC3D-BULK', 8.00, 1000, '{"Packaging":"Compostable","Quantity":"500g"}'),

    -- 503d... (Gaming Peripherals)
    ('503d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-503D-WRL', 149.00, 120, '{"Sensor":"26K-DPI","Switches":"Optical"}'),

    -- c03c... (Outdoor Apparel)
    ('c03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C03C-S', 320.00, 20, '{"Size":"S","Membrane":"3-Layer"}'),
    ('c03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C03C-M', 320.00, 35, '{"Size":"M","Membrane":"3-Layer"}'),
    ('c03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C03C-L', 320.00, 40, '{"Size":"L","Membrane":"3-Layer"}'),

    -- 1c3d... (Raw Materials)
    ('1c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-1C3D-OAK', 18.00, 500, '{"Species":"Red Oak","Moisture":"6%"}'),

    -- f83d... (Power Storage)
    ('f83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F83D-PORT', 299.00, 25, '{"Capacity":"500Wh","Weight":"5kg"}'),

    -- a03c... (Communication)
    ('a03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A03C-MESH', 450.00, 12, '{"Nodes":3,"Coverage":"5000sqft"}'),

    -- 543d... (Performance Wear)
    ('543d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-543D-SM', 28.00, 100, '{"Size":"Small","Breathability":"High"}'),
    ('543d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-543D-MD', 28.00, 150, '{"Size":"Medium","Breathability":"High"}'),

    -- f03c... (Precision Culinary)
    ('f03c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F03C-DIG', 55.00, 90, '{"Display":"Digital","Increments":"0.1g"}'),

    -- 0c3d... (Robotics)
    ('0c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-0C3D-NAV', 1800.00, 8, '{"Vision":"Lidar-HD","Suction":"5000Pa"}'),

    -- 443d... (Eyewear)
    ('443d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-443D-BLU', 95.00, 200, '{"LensType":"Blue-Light","Frame":"Titanium"}'),

    -- b03d... (Home Wellness)
    ('b03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B03D-WHT', 120.00, 45, '{"Noise":"White/Brown","IoT":"Yes"}'),

    -- 183d... (Eco-Packaging)
    ('183d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-183D-GLS', 4.50, 2000, '{"Material":"Recycled Glass","Volume":"500ml"}'),

    -- 003d... (Neuro-Wearables)
    ('003d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-003D-EEG', 850.00, 15, '{"Channels":8,"Band":"Fabric"}'),

    -- 9c3c... (Industrial Adhesive)
    ('9c3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-9C3C-EPX', 35.00, 100, '{"Type":"Epoxy","CureTime":"5min"}'),

    -- bc3c... (Optics)
    ('bc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-BC3C-50MM', 699.00, 20, '{"FocalLength":"50mm","Aperture":"f/1.8"}'),

    -- 9c3d... (Battery Tech)
    ('9c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-9C3D-AA', 15.00, 500, '{"Cell":"AA","Capacity":"2500mAh"}'),

    -- dc3c... (Active Noise Cancellation)
    ('dc3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-DC3C-PRO', 349.00, 30, '{"ANC":"Level-3","Bluetooth":"5.3"}'),

    -- 243e... (Insulation)
    ('243e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-243E-ROLL', 45.00, 100, '{"R-Value":"R-13","Material":"Rockwool"}'),

    -- 003e... (Gaming Hardware)
    ('003e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-003E-CONT', 65.00, 200, '{"Type":"Gamepad","Trigger":"Magnetic"}'),

    -- ac3c... (Sustainable Textiles)
    ('ac3c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-AC3C-YRD', 14.00, 800, '{"Fiber":"Organic-Hemp","Width":"60in"}'),

    -- 8c3d... (Edge Computing)
    ('8c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-8C3D-8G', 299.00, 40, '{"Memory":"8GB LPDDR5","Storage":"128GB"}'),

    -- 783d... (High-Speed I/O)
    ('783d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-783D-TB4', 45.00, 150, '{"Standard":"Thunderbolt 4","Length":"1m"}'),

    -- f43d... (Air Filtration)
    ('f43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F43D-HEPA', 180.00, 35, '{"Grade":"HEPA-13","Area":"500sqft"}'),

    -- 4c3d... (Sanitization)
    ('4c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-4C3D-WIPE', 6.00, 2000, '{"Count":80,"AlcoholFree":"Yes"}'),

    -- 2c3d... (Electric Mobility)
    ('2c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-2C3D-CITY', 899.00, 10, '{"Motor":"500W","Range":"20mi"}'),

    -- a43d... (Ergonomics)
    ('a43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A43D-VERT', 65.00, 120, '{"Angle":"57-deg","DPI":"4000"}'),

    -- b43c... (Computing Hardware)
    ('b43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B43C-SFF', 1100.00, 6, '{"Case":"Small-Form-Factor","Power":"Gold-750W"}'),

    -- d83d... (Portable Audio)
    ('d83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D83D-MINI', 45.00, 300, '{"IP-Rating":"IPX6","Driver":"40mm"}'),

    -- 983c... (Organic Gardening)
    ('983c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-983C-MIX', 18.00, 150, '{"Component":"Peat-Free","Volume":"20L"}'),

    -- 203e... (Technical Luggage)
    ('203e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-203E-EXP', 240.00, 25, '{"Expansion":"10L","Waterproof":"Yes"}'),

    -- d43c... (Touch Accessories)
    ('d43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D43C-GLV', 28.00, 400, '{"FingerGrip":"Conductive","Size":"M"}'),

    -- 7c3d... (Manufacturing)
    ('7c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-7C3D-RES', 55.00, 60, '{"Type":"Standard-Gray","Volume":"1L"}'),

    -- e43d... (Personal Care)
    ('e43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E43D-CRM', 14.00, 800, '{"SPF":"30","Hydration":"24hr"}'),

    -- 403d... (HVAC/Climate)
    ('403d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-403D-TOWER', 185.00, 30, '{"SpeedLevels":5,"Ionizer":"Yes"}'),

    -- 143d... (Eco-Dining)
    ('143d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-143D-STW', 12.00, 1000, '{"Material":"Glass","Count":4}'),

    -- b43d... (Electric Micro-Mobility)
    ('b43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-B43D-FOLD', 1200.00, 5, '{"Folding":"Yes","Motor":"350W"}'),

    -- 343d... (Athletic Apparel)
    ('343d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-343D-L-GRY', 35.00, 120, '{"Size":"L","Color":"Heather Gray"}'),

    -- c03d... (Gaming Audio)
    ('c03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C03D-WRL', 220.00, 40, '{"Mic":"Detachable","Surround":"7.1"}'),

    -- d43d... (Performance Nutrition)
    ('d43d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D43D-BTL', 2.50, 2000, '{"Flavor":"Blueberry","Electrolytes":"High"}'),

    -- c43c... (Visual Tech)
    ('c43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C43C-IPS', 750.00, 15, '{"PanelType":"IPS-Pro","ColorGamut":"100%-P3"}'),

    -- 103d... (Lighting Hardware)
    ('103d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-103D-MAG', 45.00, 180, '{"Base":"Magnetic","Lumens":"1000"}'),

    -- a43c... (Sustainable Production)
    ('a43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-A43C-UNIT', 12.00, 500, '{"Recyclability":"High","Origin":"Post-Consumer"}'),

    -- 0c3e... (Smart Entry)
    ('0c3e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-0C3E-FCE', 299.00, 25, '{"Auth":"Face-ID","Battery":"6-Months"}'),

    -- 643d... (Workspace Apparel)
    ('643d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-643D-M', 45.00, 100, '{"Size":"M","Fabric":"Antimicrobial"}'),

    -- c83d... (Plumbing/Filtration)
    ('c83d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-C83D-RO', 450.00, 12, '{"Method":"Reverse-Osmosis","GPD":"75"}'),

    -- dc3d... (Professional Workstations)
    ('dc3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-DC3D-X1', 3500.00, 3, '{"CPU":"Threadripper","RAM":"128GB"}'),

    -- ac3d... (Aromatherapy)
    ('ac3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-AC3D-EUC', 18.00, 200, '{"Oil":"Eucalyptus","Volume":"15ml"}'),

    -- 1c3e... (Smart Ecosystems)
    ('1c3e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-1C3E-BRG', 45.00, 150, '{"Bridge":"Matter-over-Thread","Nodes":32}'),

    -- e43c... (Culinary Science)
    ('e43c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E43C-GEN1', 250.00, 10, '{"Accuracy":"0.1C","Connectivity":"Bluetooth"}'),

    -- 603d... (Textiles)
    ('603d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-603D-COT', 12.00, 500, '{"Material":"Organic-Cotton","Size":"One-Size"}'),

    -- d03d... (Long-Range Comms)
    ('d03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D03D-SAT', 899.00, 5, '{"Network":"Iridium","SOS":"Included"}'),

    -- e83c... (Bio-Optics)
    ('e83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E83C-NIR', 120.00, 45, '{"Wave":"850nm","LED-Count":60}'),

    -- f83c... (Climate Tech)
    ('f83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-F83C-UNIT', 2100.00, 4, '{"Type":"Split-Inverter","SEER":22}'),

    -- 6c3d... (Sustainable Materials)
    ('6c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-6C3D-BAM', 15.00, 1000, '{"Source":"Moso-Bamboo","Certification":"FSC"}'),

    -- 703d... (Heavy Tools)
    ('703d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-703D-HD', 399.00, 12, '{"Torque":"200Nm","Battery":"5.0Ah"}'),

    -- 183e... (Medical/Health)
    ('183e433e-f36b-1410-8adb-00ab2e36627d', 'SKU-183E-BIO', 55.00, 250, '{"Metric":"Pulse-Ox","Accuracy":"High"}'),

    -- 5c3d... (Health Consumables)
    ('5c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-5C3D-MNT', 4.99, 2000, '{"SPF":"15","Flavor":"Peppermint"}'),

    -- 043d... (EV Hardware)
    ('043d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-043D-LV2', 650.00, 15, '{"Level":"Level 2","Amps":"48A"}'),

    -- 303d... (Kitchen Finishes)
    ('303d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-303D-BRS', 85.00, 100, '{"Finish":"Brushed-Brass","Mount":"Under-mount"}'),

    -- 383d... (High-Performance Mesh)
    ('383d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-383D-TRI', 450.00, 30, '{"Nodes":3,"Speed":"AXE11000"}'),

    -- e03d... (Tactical Gear)
    ('e03d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-E03D-MIL', 110.00, 50, '{"Grade":"Mil-Spec","Stitching":"Double"}'),

    -- d83c... (Energy Devices)
    ('d83c433e-f36b-1410-8adb-00ab2e36627d', 'SKU-D83C-10W', 45.00, 200, '{"Wattage":"10W","Type":"Solar-Panel"}'),

    -- 3c3d... (Optical Precision)
    ('3c3d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-3C3D-70-200', 1450.00, 6, '{"FocalRange":"70-200mm","Stops":"f/4"}'),

    -- 903d... (Filtration Media)
    ('903d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-903D-CRB', 15.00, 1000, '{"Media":"Activated-Carbon","PoreSize":"5-Micron"}'),

    -- 743d... (Advanced Displays)
    ('743d433e-f36b-1410-8adb-00ab2e36627d', 'SKU-743D-4K', 1100.00, 10, '{"Resolution":"4K-UHD","Refresh":"240Hz"}');


-- Table: ProductTags (CatalogItemId, Name)

INSERT INTO cat.ProductTags
    (CatalogItemId, Name)
VALUES
    -- a03d... (Smart Home/Networking)
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'SmartHome'),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'Networking'),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'WiFi7'),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', 'IoT'),

    -- 883d... (Electronics/Display)
    ('883d433e-f36b-1410-8adb-00ab2e36627d', 'Display'),
    ('883d433e-f36b-1410-8adb-00ab2e36627d', 'OLED'),
    ('883d433e-f36b-1410-8adb-00ab2e36627d', 'Gaming'),

    -- 083e... (Adventure/Sustainable)
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'Outdoor'),
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'Recycled'),
    ('083e433e-f36b-1410-8adb-00ab2e36627d', 'EcoFriendly'),

    -- fc3d... (Health/Bio)
    ('fc3d433e-f36b-1410-8adb-00ab2e36627d', 'HealthTech'),
    ('fc3d433e-f36b-1410-8adb-00ab2e36627d', 'BioHacking'),
    ('fc3d433e-f36b-1410-8adb-00ab2e36627d', 'Sensors'),

    -- 683d... (Apparel)
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'Apparel'),
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'SustainableFashion'),
    ('683d433e-f36b-1410-8adb-00ab2e36627d', 'MerinoWool'),

    -- f43c... (Wellness)
    ('f43c433e-f36b-1410-8adb-00ab2e36627d', 'Wellness'),
    ('f43c433e-f36b-1410-8adb-00ab2e36627d', 'Nootropics'),

    -- b83c... (Tools)
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', 'PowerTools'),
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', 'Professional'),
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', 'Brushless'),

    -- 143e... (Home)
    ('143e433e-f36b-1410-8adb-00ab2e36627d', 'Aromatherapy'),
    ('143e433e-f36b-1410-8adb-00ab2e36627d', 'HomeDecor'),

    -- c43d... (Tech Hardware)
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', 'Hardware'),
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', 'Storage'),
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', 'HighSpeed'),

    -- cc3c... (Energy)
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', 'PowerSupply'),
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', 'FastCharging'),

    -- 203d... (DIY/Materials)
    ('203d433e-f36b-1410-8adb-00ab2e36627d', 'Materials'),
    ('203d433e-f36b-1410-8adb-00ab2e36627d', 'Finish'),

    -- 803d... (Wearables)
    ('803d433e-f36b-1410-8adb-00ab2e36627d', 'WearableTech'),
    ('803d433e-f36b-1410-8adb-00ab2e36627d', 'HealthMonitoring'),

    -- e83d... (Mobility)
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'ElectricMobility'),
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'Transportation'),
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'CarbonFiber'),

    -- 943d... (Water)
    ('943d433e-f36b-1410-8adb-00ab2e36627d', 'Filtration'),
    ('943d433e-f36b-1410-8adb-00ab2e36627d', 'PureWater'),

    -- a83c... (Nutrition)
    ('a83c433e-f36b-1410-8adb-00ab2e36627d', 'Nutrition'),
    ('a83c433e-f36b-1410-8adb-00ab2e36627d', 'BioScience'),

    -- b03c... (UI/Hardware)
    ('b03c433e-f36b-1410-8adb-00ab2e36627d', 'Haptics'),
    ('b03c433e-f36b-1410-8adb-00ab2e36627d', 'Interface'),

    -- 583d... (Fitness)
    ('583d433e-f36b-1410-8adb-00ab2e36627d', 'Fitness'),
    ('583d433e-f36b-1410-8adb-00ab2e36627d', 'Yoga'),

    -- f03d... (Audio)
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'Audiophile'),
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'HiFi'),
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'Headphones'),

    -- c83c... (Lighting)
    ('c83c433e-f36b-1410-8adb-00ab2e36627d', 'SmartLighting'),
    ('c83c433e-f36b-1410-8adb-00ab2e36627d', 'EnergySaving'),

    -- cc3d... (Computing)
    ('cc3d433e-f36b-1410-8adb-00ab2e36627d', 'Workstation'),
    ('cc3d433e-f36b-1410-8adb-00ab2e36627d', 'VaporChamber'),

    -- b83d... (Culinary)
    ('b83d433e-f36b-1410-8adb-00ab2e36627d', 'KitchenTech'),
    ('b83d433e-f36b-1410-8adb-00ab2e36627d', 'PrecisionCooking'),

    -- a83d... (Tools/Light)
    ('a83d433e-f36b-1410-8adb-00ab2e36627d', 'PortableLight'),
    ('a83d433e-f36b-1410-8adb-00ab2e36627d', 'USB-C'),

    -- 283d... (Optics)
    ('283d433e-f36b-1410-8adb-00ab2e36627d', 'Photography'),
    ('283d433e-f36b-1410-8adb-00ab2e36627d', 'Zeiss'),

    -- 843d... (Security)
    ('843d433e-f36b-1410-8adb-00ab2e36627d', 'Security'),
    ('843d433e-f36b-1410-8adb-00ab2e36627d', 'ThermalCamera'),

    -- 483d... (Eco/Health)
    ('483d433e-f36b-1410-8adb-00ab2e36627d', 'EcoLiving'),
    ('483d433e-f36b-1410-8adb-00ab2e36627d', 'AirQuality'),

    -- 043e... (Automation)
    ('043e433e-f36b-1410-8adb-00ab2e36627d', 'Automation'),
    ('043e433e-f36b-1410-8adb-00ab2e36627d', 'MatterProtocol'),

    -- bc3d... (Sports/Health)
    ('bc3d433e-f36b-1410-8adb-00ab2e36627d', 'Athletic'),
    ('bc3d433e-f36b-1410-8adb-00ab2e36627d', 'Compression'),

    -- e03c... (HVAC)
    ('e03c433e-f36b-1410-8adb-00ab2e36627d', 'ThermalControl'),
    ('e03c433e-f36b-1410-8adb-00ab2e36627d', 'EnergyEfficiency'),

    -- ec3c... (HiFi)
    ('ec3c433e-f36b-1410-8adb-00ab2e36627d', 'AudioGrade'),
    ('ec3c433e-f36b-1410-8adb-00ab2e36627d', 'DAC'),

    -- 243d... (Space/Comms)
    ('243d433e-f36b-1410-8adb-00ab2e36627d', 'Satellite'),
    ('243d433e-f36b-1410-8adb-00ab2e36627d', 'Telecomm'),

    -- 983d... (Adventure)
    ('983d433e-f36b-1410-8adb-00ab2e36627d', 'Hiking'),
    ('983d433e-f36b-1410-8adb-00ab2e36627d', 'UVProtection'),

    -- 103e... (Design)
    ('103e433e-f36b-1410-8adb-00ab2e36627d', 'Architectural'),
    ('103e433e-f36b-1410-8adb-00ab2e36627d', 'Modular'),

    -- d03c... (Kitchen)
    ('d03c433e-f36b-1410-8adb-00ab2e36627d', 'ChefTools'),
    ('d03c433e-f36b-1410-8adb-00ab2e36627d', 'Ceramic'),

    -- 083d... (Green Energy)
    ('083d433e-f36b-1410-8adb-00ab2e36627d', 'Renewable'),
    ('083d433e-f36b-1410-8adb-00ab2e36627d', 'SolarPower'),

    -- fc3c... (Smart Home/Air)
    ('fc3c433e-f36b-1410-8adb-00ab2e36627d', 'AirMonitoring'),
    ('fc3c433e-f36b-1410-8adb-00ab2e36627d', 'SmartSensors'),

    -- ec3d... (Eco/Life)
    ('ec3d433e-f36b-1410-8adb-00ab2e36627d', 'ZeroWaste'),
    ('ec3d433e-f36b-1410-8adb-00ab2e36627d', 'Compostable'),

    -- 503d... (Gaming/Interface)
    ('503d433e-f36b-1410-8adb-00ab2e36627d', 'HighDPI'),
    ('503d433e-f36b-1410-8adb-00ab2e36627d', 'OpticalSwitches'),

    -- c03c... (Adventure Apparel)
    ('c03c433e-f36b-1410-8adb-00ab2e36627d', 'Outdoorsman'),
    ('c03c433e-f36b-1410-8adb-00ab2e36627d', 'Waterproof'),

    -- 1c3d... (Lumber/DIY)
    ('1c3d433e-f36b-1410-8adb-00ab2e36627d', 'Woodwork'),
    ('1c3d433e-f36b-1410-8adb-00ab2e36627d', 'FSC-Certified'),

    -- f83d... (Power/Tech)
    ('f83d433e-f36b-1410-8adb-00ab2e36627d', 'PortableEnergy'),
    ('f83d433e-f36b-1410-8adb-00ab2e36627d', 'OffGrid'),

    -- a03c... (Mesh/Network)
    ('a03c433e-f36b-1410-8adb-00ab2e36627d', 'MeshNetwork'),
    ('a03c433e-f36b-1410-8adb-00ab2e36627d', 'Connectivity'),

    -- 543d... (Athletic)
    ('543d433e-f36b-1410-8adb-00ab2e36627d', 'Performance'),
    ('543d433e-f36b-1410-8adb-00ab2e36627d', 'Activewear'),

    -- f03c... (Kitchen/Scale)
    ('f03c433e-f36b-1410-8adb-00ab2e36627d', 'Baking'),
    ('f03c433e-f36b-1410-8adb-00ab2e36627d', 'MolecularGastronomy'),

    -- 0c3d... (Robotics/AI)
    ('0c3d433e-f36b-1410-8adb-00ab2e36627d', 'Robotics'),
    ('0c3d433e-f36b-1410-8adb-00ab2e36627d', 'Lidar'),
    ('0c3d433e-f36b-1410-8adb-00ab2e36627d', 'AI-Navigation'),

    -- 443d... (Optics)
    ('443d433e-f36b-1410-8adb-00ab2e36627d', 'EyeCare'),
    ('443d433e-f36b-1410-8adb-00ab2e36627d', 'BlueLight'),

    -- b03d... (Health/Home)
    ('b03d433e-f36b-1410-8adb-00ab2e36627d', 'SleepAid'),
    ('b03d433e-f36b-1410-8adb-00ab2e36627d', 'Meditation'),

    -- 183d... (Packaging)
    ('183d433e-f36b-1410-8adb-00ab2e36627d', 'SustainablePackaging'),
    ('183d433e-f36b-1410-8adb-00ab2e36627d', 'BPA-Free'),

    -- 003d... (Neural/Health)
    ('003d433e-f36b-1410-8adb-00ab2e36627d', 'NeuralLink'),
    ('003d433e-f36b-1410-8adb-00ab2e36627d', 'DeepWork'),

    -- 9c3c... (Industrial)
    ('9c3c433e-f36b-1410-8adb-00ab2e36627d', 'IndustrialStrength'),
    ('9c3c433e-f36b-1410-8adb-00ab2e36627d', 'Adhesive'),

    -- bc3c... (Optics)
    ('bc3c433e-f36b-1410-8adb-00ab2e36627d', 'PhotographyLenses'),
    ('bc3c433e-f36b-1410-8adb-00ab2e36627d', 'ProfessionalOptics'),

    -- 9c3d... (Power)
    ('9c3d433e-f36b-1410-8adb-00ab2e36627d', 'BatteryLife'),
    ('9c3d433e-f36b-1410-8adb-00ab2e36627d', 'Rechargeable'),

    -- dc3c... (Audio)
    ('dc3c433e-f36b-1410-8adb-00ab2e36627d', 'ANC'),
    ('dc3c433e-f36b-1410-8adb-00ab2e36627d', 'Bluetooth5.3'),

    -- 243e... (Home Improve)
    ('243e433e-f36b-1410-8adb-00ab2e36627d', 'Insulation'),
    ('243e433e-f36b-1410-8adb-00ab2e36627d', 'EnergyAudit'),

    -- 003e... (Gaming)
    ('003e433e-f36b-1410-8adb-00ab2e36627d', 'Controller'),
    ('003e433e-f36b-1410-8adb-00ab2e36627d', 'Tactile'),

    -- ac3c... (Textiles)
    ('ac3c433e-f36b-1410-8adb-00ab2e36627d', 'OrganicFabric'),
    ('ac3c433e-f36b-1410-8adb-00ab2e36627d', 'IndustrialHemp'),

    -- 8c3d... (Computing)
    ('8c3d433e-f36b-1410-8adb-00ab2e36627d', 'EdgeComputing'),
    ('8c3d433e-f36b-1410-8adb-00ab2e36627d', 'ARM-Processor'),

    -- 783d... (Hardware)
    ('783d433e-f36b-1410-8adb-00ab2e36627d', 'Thunderbolt4'),
    ('783d433e-f36b-1410-8adb-00ab2e36627d', 'DataTransfer'),

    -- f43d... (Clean Air)
    ('f43d433e-f36b-1410-8adb-00ab2e36627d', 'HEPA13'),
    ('f43d433e-f36b-1410-8adb-00ab2e36627d', 'CleanAir'),

    -- 4c3d... (Wellness)
    ('4c3d433e-f36b-1410-8adb-00ab2e36627d', 'Sanitize'),
    ('4c3d433e-f36b-1410-8adb-00ab2e36627d', 'GermProtection'),

    -- 2c3d... (Micro-Mobility)
    ('2c3d433e-f36b-1410-8adb-00ab2e36627d', 'UrbanCommute'),
    ('2c3d433e-f36b-1410-8adb-00ab2e36627d', 'ElectricBike'),

    -- a43d... (Ergo)
    ('a43d433e-f36b-1410-8adb-00ab2e36627d', 'ErgonomicMouse'),
    ('a43d433e-f36b-1410-8adb-00ab2e36627d', 'CarpalTunnelPrevention'),

    -- b43c... (SFF Tech)
    ('b43c433e-f36b-1410-8adb-00ab2e36627d', 'SFF-Computing'),
    ('b43c433e-f36b-1410-8adb-00ab2e36627d', 'LiquidCooling'),

    -- d83d... (Audio)
    ('d83d433e-f36b-1410-8adb-00ab2e36627d', 'PortableAudio'),
    ('d83d433e-f36b-1410-8adb-00ab2e36627d', 'IPX6'),

    -- 983c... (Gardening)
    ('983c433e-f36b-1410-8adb-00ab2e36627d', 'Gardening'),
    ('983c433e-f36b-1410-8adb-00ab2e36627d', 'OrganicSoil'),

    -- 203e... (Luggage)
    ('203e433e-f36b-1410-8adb-00ab2e36627d', 'TravelGear'),
    ('203e433e-f36b-1410-8adb-00ab2e36627d', 'DigitalNomad'),

    -- d43c... (Touch Tech)
    ('d43c433e-f36b-1410-8adb-00ab2e36627d', 'ConductiveApparel'),

    -- 7c3d... (Additive Mfg)
    ('7c3d433e-f36b-1410-8adb-00ab2e36627d', '3DPrinting'),
    ('7c3d433e-f36b-1410-8adb-00ab2e36627d', 'SLA-Resin'),

    -- e43d... (Skincare)
    ('e43d433e-f36b-1410-8adb-00ab2e36627d', 'SkinCare'),
    ('e43d433e-f36b-1410-8adb-00ab2e36627d', 'Moisturizer'),

    -- 403d... (Air Flow)
    ('403d433e-f36b-1410-8adb-00ab2e36627d', 'TowerFan'),
    ('403d433e-f36b-1410-8adb-00ab2e36627d', 'Cooling'),

    -- 143d... (Dining)
    ('143d433e-f36b-1410-8adb-00ab2e36627d', 'SustainableDining'),

    -- b43d... (E-Bikes)
    ('b43d433e-f36b-1410-8adb-00ab2e36627d', 'FoldingBike'),
    ('b43d433e-f36b-1410-8adb-00ab2e36627d', 'E-Bike'),

    -- 343d... (Sportswear)
    ('343d433e-f36b-1410-8adb-00ab2e36627d', 'ActiveApparel'),

    -- c03d... (Gaming Audio)
    ('c03d433e-f36b-1410-8adb-00ab2e36627d', 'GamingHeadset'),
    ('c03d433e-f36b-1410-8adb-00ab2e36627d', 'SpatialAudio'),

    -- d43d... (Bio-Hydration)
    ('d43d433e-f36b-1410-8adb-00ab2e36627d', 'Hydration'),
    ('d43d433e-f36b-1410-8adb-00ab2e36627d', 'AthleticFuel'),

    -- c43c... (Display Pro)
    ('c43c433e-f36b-1410-8adb-00ab2e36627d', 'ColorGrading'),
    ('c43c433e-f36b-1410-8adb-00ab2e36627d', 'IPS-Pro'),

    -- 103d... (Work Light)
    ('103d433e-f36b-1410-8adb-00ab2e36627d', 'IndustrialLight'),

    -- a43c... (Sustainability)
    ('a43c433e-f36b-1410-8adb-00ab2e36627d', 'CircularEconomy'),

    -- 0c3e... (Security Face)
    ('0c3e433e-f36b-1410-8adb-00ab2e36627d', 'FacialRecognition'),
    ('0c3e433e-f36b-1410-8adb-00ab2e36627d', 'SmartSecurity'),

    -- 643d... (Apparel)
    ('643d433e-f36b-1410-8adb-00ab2e36627d', 'ProfessionalApparel'),

    -- c83d... (Plumbing)
    ('c83d433e-f36b-1410-8adb-00ab2e36627d', 'ReverseOsmosis'),
    ('c83d433e-f36b-1410-8adb-00ab2e36627d', 'WaterScience'),

    -- dc3d... (Workstations)
    ('dc3d433e-f36b-1410-8adb-00ab2e36627d', 'Threadripper'),
    ('dc3d433e-f36b-1410-8adb-00ab2e36627d', 'ProductionPC'),

    -- ac3d... (Aroma)
    ('ac3d433e-f36b-1410-8adb-00ab2e36627d', 'StressRelief'),

    -- 1c3e... (Matter/Thread)
    ('1c3e433e-f36b-1410-8adb-00ab2e36627d', 'ConnectivityHub'),

    -- e43c... (Sous Vide)
    ('e43c433e-f36b-1410-8adb-00ab2e36627d', 'SousVide'),
    ('e43c433e-f36b-1410-8adb-00ab2e36627d', 'ModernKitchen'),

    -- 603d... (Textiles)
    ('603d433e-f36b-1410-8adb-00ab2e36627d', 'NaturalFiber'),

    -- d03d... (Satellite Comms)
    ('d03d433e-f36b-1410-8adb-00ab2e36627d', 'OutdoorSafety'),

    -- e83c... (Bio Hacking)
    ('e83c433e-f36b-1410-8adb-00ab2e36627d', 'InfraredTherapy'),

    -- f83c... (HVAC)
    ('f83c433e-f36b-1410-8adb-00ab2e36627d', 'AirConditioning'),

    -- 6c3d... (Sustainable DIY)
    ('6c3d433e-f36b-1410-8adb-00ab2e36627d', 'SustainableLumber'),

    -- 703d... (Power Tools)
    ('703d433e-f36b-1410-8adb-00ab2e36627d', 'Construction'),

    -- 183e... (Health Data)
    ('183e433e-f36b-1410-8adb-00ab2e36627d', 'HealthData'),

    -- 5c3d... (Daily Care)
    ('5c3d433e-f36b-1410-8adb-00ab2e36627d', 'PersonalHygiene'),

    -- 043d... (EV Tech)
    ('043d433e-f36b-1410-8adb-00ab2e36627d', 'EVCharging'),

    -- 303d... (Home Design)
    ('303d433e-f36b-1410-8adb-00ab2e36627d', 'LuxuryFinishes'),

    -- 383d... (Home Networking)
    ('383d433e-f36b-1410-8adb-00ab2e36627d', 'HighCoverageWiFi'),

    -- e03d... (Tactical)
    ('e03d433e-f36b-1410-8adb-00ab2e36627d', 'TacticalGear'),

    -- d83c... (Solar Energy)
    ('d83c433e-f36b-1410-8adb-00ab2e36627d', 'SolarTech'),

    -- 3c3d... (Photography)
    ('3c3d433e-f36b-1410-8adb-00ab2e36627d', 'ApoLens'),

    -- 903d... (Filtration)
    ('903d433e-f36b-1410-8adb-00ab2e36627d', 'WaterQuality'),

    -- 743d... (Displays)
    ('743d433e-f36b-1410-8adb-00ab2e36627d', 'MonitorTech');

INSERT INTO cat.ProductImages
    (ProductVariantId, ImageUrl, AltText, IsPrimary, DisplayOrder)
VALUES
    -- Tech / Electronics Variants
    ('7f40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?auto=format&fit=crop&w=800', 'High-performance mesh router with sleek antennas', 1, 1),
    ('8240433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?auto=format&fit=crop&w=800', 'Neural link cognitive wearable device on dark background', 1, 1),
    ('8540433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1585333120166-4336c45d5338?auto=format&fit=crop&w=800', 'Bladeless ionic air purifier tower', 1, 1),
    ('8840433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=800', 'Professional workstation laptop with 16-inch display', 1, 1),
    ('8b40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=800', 'Smart fitness watch with biometrics display', 1, 1),

    -- Outdoor / Adventure Variants
    ('8e40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1551632811-561732d1e306?auto=format&fit=crop&w=800', 'Rugged waterproof technical hiking boots', 1, 1),
    ('9140433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1501555088652-021faa106b9b?auto=format&fit=crop&w=800', 'All-weather alpine parka in volcanic gray', 1, 1),
    ('9440433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1622260614153-03223fb72052?auto=format&fit=crop&w=800', 'Ultra-lightweight 45L expedition backpack', 1, 1),
    ('9740433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1504280390367-361c6d9f38f4?auto=format&fit=crop&w=800', 'Compact two-person ultralight camping tent', 1, 1),

    -- Home / Culinary Variants
    ('9a40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1591261730799-ee4e6c2d16d7?auto=format&fit=crop&w=800', 'Digital immersion sous-vide circulator in pot', 1, 1),
    ('9d40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1544232475-cd42991e3430?auto=format&fit=crop&w=800', 'Modern air fryer station with dual baskets', 1, 1),
    ('a040433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1544725176-7c40e5a71c5e?auto=format&fit=crop&w=800', 'Audiophile planar magnetic headphones', 1, 1),

    -- Industrial / Tool Variants
    ('a340433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1504148455328-4972fbb2d5fc?auto=format&fit=crop&w=800', '20V brushless impact drill with high torque', 1, 1),
    ('a640433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1581244273443-cba399a466bd?auto=format&fit=crop&w=800', 'Precision desktop CNC lathe for model making', 1, 1),

    -- Apparel Variants
    ('a940433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&w=800', 'Thermal regulating vest with smart fabric', 1, 1),
    ('ac40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1542272604-787c3835535d?auto=format&fit=crop&w=800', 'Commuter trousers with four-way stretch', 1, 1),

    -- Health / Wellness Variants
    ('af40433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1551076805-e18690637596?auto=format&fit=crop&w=800', 'Non-invasive glucose tracker wearable', 1, 1),
    ('b240433e-f36b-1410-8adb-00ab2e36627d', 'https://images.unsplash.com/photo-1512290923902-8a9f81dc2069?auto=format&fit=crop&w=800', 'Red light therapy mask for skin rejuvenation', 1, 1),

    -- [Pattern follows for all 100+ IDs using unique Unsplash seeds]
    ('b540433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/b540/800/600', 'Variant Image SKU B540', 1, 1),
    ('b840433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/b840/800/600', 'Variant Image SKU B840', 1, 1),
    ('bb40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/bb40/800/600', 'Variant Image SKU BB40', 1, 1),
    ('be40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/be40/800/600', 'Variant Image SKU BE40', 1, 1),
    ('c140433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c140/800/600', 'Variant Image SKU C140', 1, 1),
    ('c440433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c440/800/600', 'Variant Image SKU C440', 1, 1),
    ('c740433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c740/800/600', 'Variant Image SKU C740', 1, 1),
    ('ca40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ca40/800/600', 'Variant Image SKU CA40', 1, 1),
    ('cd40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/cd40/800/600', 'Variant Image SKU CD40', 1, 1),
    ('d040433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d040/800/600', 'Variant Image SKU D040', 1, 1),
    ('d340433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d340/800/600', 'Variant Image SKU D340', 1, 1),
    ('d640433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d640/800/600', 'Variant Image SKU D640', 1, 1),
    ('d940433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d940/800/600', 'Variant Image SKU D940', 1, 1),
    ('dc40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/dc40/800/600', 'Variant Image SKU DC40', 1, 1),
    ('df40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/df40/800/600', 'Variant Image SKU DF40', 1, 1),
    ('e240433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e240/800/600', 'Variant Image SKU E240', 1, 1),
    ('e540433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e540/800/600', 'Variant Image SKU E540', 1, 1),
    ('e840433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e840/800/600', 'Variant Image SKU E840', 1, 1),
    ('eb40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/eb40/800/600', 'Variant Image SKU EB40', 1, 1),
    ('ee40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ee40/800/600', 'Variant Image SKU EE40', 1, 1),
    ('f140433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f140/800/600', 'Variant Image SKU F140', 1, 1),
    ('f440433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f440/800/600', 'Variant Image SKU F440', 1, 1),
    ('f740433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f740/800/600', 'Variant Image SKU F740', 1, 1),
    ('fa40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/fa40/800/600', 'Variant Image SKU FA40', 1, 1),
    ('fd40433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/fd40/800/600', 'Variant Image SKU FD40', 1, 1),
    ('0041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0041/800/600', 'Variant Image SKU 0041', 1, 1),
    ('0341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0341/800/600', 'Variant Image SKU 0341', 1, 1),
    ('0641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0641/800/600', 'Variant Image SKU 0641', 1, 1),
    ('0941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0941/800/600', 'Variant Image SKU 0941', 1, 1),
    ('0c41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0c41/800/600', 'Variant Image SKU 0C41', 1, 1),
    ('0f41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/0f41/800/600', 'Variant Image SKU 0F41', 1, 1),
    ('1241433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/1241/800/600', 'Variant Image SKU 1241', 1, 1),
    ('1541433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/1541/800/600', 'Variant Image SKU 1541', 1, 1),
    ('1841433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/1841/800/600', 'Variant Image SKU 1841', 1, 1),
    ('1b41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/1b41/800/600', 'Variant Image SKU 1B41', 1, 1),
    ('1e41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/1e41/800/600', 'Variant Image SKU 1E41', 1, 1),
    ('2141433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/2141/800/600', 'Variant Image SKU 2141', 1, 1),
    ('2441433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/2441/800/600', 'Variant Image SKU 2441', 1, 1),
    ('2741433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/2741/800/600', 'Variant Image SKU 2741', 1, 1),
    ('2a41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/2a41/800/600', 'Variant Image SKU 2A41', 1, 1),
    ('2d41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/2d41/800/600', 'Variant Image SKU 2D41', 1, 1),
    ('3041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3041/800/600', 'Variant Image SKU 3041', 1, 1),
    ('3341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3341/800/600', 'Variant Image SKU 3341', 1, 1),
    ('3641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3641/800/600', 'Variant Image SKU 3641', 1, 1),
    ('3941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3941/800/600', 'Variant Image SKU 3941', 1, 1),
    ('3c41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3c41/800/600', 'Variant Image SKU 3C41', 1, 1),
    ('3f41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/3f41/800/600', 'Variant Image SKU 3F41', 1, 1),
    ('4241433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/4241/800/600', 'Variant Image SKU 4241', 1, 1),
    ('4541433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/4541/800/600', 'Variant Image SKU 4541', 1, 1),
    ('4841433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/4841/800/600', 'Variant Image SKU 4841', 1, 1),
    ('4b41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/4b41/800/600', 'Variant Image SKU 4B41', 1, 1),
    ('4e41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/4e41/800/600', 'Variant Image SKU 4E41', 1, 1),
    ('5141433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/5141/800/600', 'Variant Image SKU 5141', 1, 1),
    ('5441433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/5441/800/600', 'Variant Image SKU 5441', 1, 1),
    ('5741433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/5741/800/600', 'Variant Image SKU 5741', 1, 1),
    ('5a41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/5a41/800/600', 'Variant Image SKU 5A41', 1, 1),
    ('5d41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/5d41/800/600', 'Variant Image SKU 5D41', 1, 1),
    ('6041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6041/800/600', 'Variant Image SKU 6041', 1, 1),
    ('6341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6341/800/600', 'Variant Image SKU 6341', 1, 1),
    ('6641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6641/800/600', 'Variant Image SKU 6641', 1, 1),
    ('6941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6941/800/600', 'Variant Image SKU 6941', 1, 1),
    ('6c41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6c41/800/600', 'Variant Image SKU 6C41', 1, 1),
    ('6f41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/6f41/800/600', 'Variant Image SKU 6F41', 1, 1),
    ('7241433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/7241/800/600', 'Variant Image SKU 7241', 1, 1),
    ('7541433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/7541/800/600', 'Variant Image SKU 7541', 1, 1),
    ('7841433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/7841/800/600', 'Variant Image SKU 7841', 1, 1),
    ('7b41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/7b41/800/600', 'Variant Image SKU 7B41', 1, 1),
    ('7e41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/7e41/800/600', 'Variant Image SKU 7E41', 1, 1),
    ('8141433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/8141/800/600', 'Variant Image SKU 8141', 1, 1),
    ('8441433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/8441/800/600', 'Variant Image SKU 8441', 1, 1),
    ('8741433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/8741/800/600', 'Variant Image SKU 8741', 1, 1),
    ('8a41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/8a41/800/600', 'Variant Image SKU 8A41', 1, 1),
    ('8d41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/8d41/800/600', 'Variant Image SKU 8D41', 1, 1),
    ('9041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9041/800/600', 'Variant Image SKU 9041', 1, 1),
    ('9341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9341/800/600', 'Variant Image SKU 9341', 1, 1),
    ('9641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9641/800/600', 'Variant Image SKU 9641', 1, 1),
    ('9941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9941/800/600', 'Variant Image SKU 9941', 1, 1),
    ('9c41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9c41/800/600', 'Variant Image SKU 9C41', 1, 1),
    ('9f41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/9f41/800/600', 'Variant Image SKU 9F41', 1, 1),
    ('a241433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/a241/800/600', 'Variant Image SKU A241', 1, 1),
    ('a541433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/a541/800/600', 'Variant Image SKU A541', 1, 1),
    ('a841433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/a841/800/600', 'Variant Image SKU A841', 1, 1),
    ('ab41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ab41/800/600', 'Variant Image SKU AB41', 1, 1),
    ('ae41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ae41/800/600', 'Variant Image SKU AE41', 1, 1),
    ('b141433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/b141/800/600', 'Variant Image SKU B141', 1, 1),
    ('b441433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/b441/800/600', 'Variant Image SKU B441', 1, 1),
    ('b741433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/b741/800/600', 'Variant Image SKU B741', 1, 1),
    ('ba41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ba41/800/600', 'Variant Image SKU BA41', 1, 1),
    ('bd41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/bd41/800/600', 'Variant Image SKU BD41', 1, 1),
    ('c041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c041/800/600', 'Variant Image SKU C041', 1, 1),
    ('c341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c341/800/600', 'Variant Image SKU C341', 1, 1),
    ('c641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c641/800/600', 'Variant Image SKU C641', 1, 1),
    ('c941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/c941/800/600', 'Variant Image SKU C941', 1, 1),
    ('cc41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/cc41/800/600', 'Variant Image SKU CC41', 1, 1),
    ('cf41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/cf41/800/600', 'Variant Image SKU CF41', 1, 1),
    ('d241433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d241/800/600', 'Variant Image SKU D241', 1, 1),
    ('d541433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d541/800/600', 'Variant Image SKU D541', 1, 1),
    ('d841433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/d841/800/600', 'Variant Image SKU D841', 1, 1),
    ('db41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/db41/800/600', 'Variant Image SKU DB41', 1, 1),
    ('de41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/de41/800/600', 'Variant Image SKU DE41', 1, 1),
    ('e141433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e141/800/600', 'Variant Image SKU E141', 1, 1),
    ('e441433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e441/800/600', 'Variant Image SKU E441', 1, 1),
    ('e741433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/e741/800/600', 'Variant Image SKU E741', 1, 1),
    ('ea41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ea41/800/600', 'Variant Image SKU EA41', 1, 1),
    ('ed41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ed41/800/600', 'Variant Image SKU ED41', 1, 1),
    ('f041433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f041/800/600', 'Variant Image SKU F041', 1, 1),
    ('f341433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f341/800/600', 'Variant Image SKU F341', 1, 1),
    ('f641433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f641/800/600', 'Variant Image SKU F641', 1, 1),
    ('f941433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/f941/800/600', 'Variant Image SKU F941', 1, 1),
    ('fc41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/fc41/800/600', 'Variant Image SKU FC41', 1, 1),
    ('ff41433e-f36b-1410-8adb-00ab2e36627d', 'https://picsum.photos/seed/ff41/800/600', 'Variant Image SKU FF41', 1, 1);

INSERT INTO cat.ProductReviews
    (CatalogItemId, CustomerId, Rating, ReviewerAlias, ReviewText, CreatedAt)
VALUES
    -- a03d... (QuantumSync Router)
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', '0fcc2f86-c2b8-4ec1-abe5-299790d94f75', 5, 'TechGuru88', 'The Wi-Fi 7 speeds are no joke. I finally have a stable connection in my basement office through three concrete walls. Setup via the app was seamless.', GETDATE()),
    ('a03d433e-f36b-1410-8adb-00ab2e36627d', '152fd534-3fcf-468a-986b-3735a909bf79', 4, 'HomeUser_Jan', 'Great coverage, but the units are a bit larger than I expected. Signal is rock solid though.', GETDATE()),

    -- 883d... (OLED Display)
    ('883d433e-f36b-1410-8adb-00ab2e36627d', '2814462e-3db7-42d2-9bfd-be335f817d0a', 5, 'PixelPerfect', 'The black levels on this OLED are incredible. Using it for color grading and the DCI-P3 accuracy is spot on right out of the box.', GETDATE()),
    ('883d433e-f36b-1410-8adb-00ab2e36627d', '2d9e354b-c456-4457-9ddf-166152952dfd', 2, 'GamerX', 'Received with two dead pixels. For this price, quality control should be better. Returning for a replacement.', GETDATE()),

    -- 083e... (Adventure Pack)
    ('083e433e-f36b-1410-8adb-00ab2e36627d', '30dfa509-23dd-4113-a2c7-b894e6df1730', 5, 'MountainMan', 'Took this on a 3-day trek. The weight distribution is perfect, barely felt the 30lb load on my shoulders.', GETDATE()),

    -- fc3d... (Health Sensors)
    ('fc3d433e-f36b-1410-8adb-00ab2e36627d', '32ac2e8a-5592-49ff-88fd-80936391aabb', 4, 'BioHacker_v2', 'The interstitial monitoring is highly accurate compared to my finger-prick device. The app UI needs a bit of work though.', GETDATE()),

    -- 683d... (Merino Apparel)
    ('683d433e-f36b-1410-8adb-00ab2e36627d', '34200a89-716b-4dfa-ab52-b23663d73bd2', 5, 'EcoTraveler', 'Wore this for three days straight while traveling—no odor at all. The merino blend is soft and definitely not itchy.', GETDATE()),

    -- f43c... (Nootropics)
    ('f43c433e-f36b-1410-8adb-00ab2e36627d', '3dd42643-ca4f-447b-8e68-8c66f56b1d6e', 3, 'StudyHard', 'I feel a slight increase in focus, but it might be a placebo. No jittery feeling though, which is good.', GETDATE()),

    -- b83c... (Titan Tools)
    ('b83c433e-f36b-1410-8adb-00ab2e36627d', '59deea0b-a1eb-4191-a84f-c3265b186501', 5, 'ProContractor', 'This drill has more torque than my corded model. The haptic kickback protection saved my wrist yesterday on a masonry job.', GETDATE()),

    -- 143e... (Home Fragrance)
    ('143e433e-f36b-1410-8adb-00ab2e36627d', '6b0211f7-f85c-4fd2-b952-f5945d6e1fe7', 5, 'ScentLover', 'The Sandalwood scent is deep and natural. It fills the room without being overwhelming. Buying the Lavender next.', GETDATE()),

    -- c43d... (SSD Storage)
    ('c43d433e-f36b-1410-8adb-00ab2e36627d', '8fb738da-44a3-483a-a24e-dee77122fd5b', 4, 'VideoEditor', 'Fast transfer speeds for 4K raw footage. Fingerprint sensor is a nice security touch for travel.', GETDATE()),

    -- cc3c... (Fast Charger)
    ('cc3c433e-f36b-1410-8adb-00ab2e36627d', '9c1c2b6f-6852-45cc-a7d8-4c7c64b5ae4d', 5, 'DigitalNomad', 'Finally a single brick that charges my laptop and phone at full speed. Compact enough for my tech pouch.', GETDATE()),

    -- e83d... (E-Bike)
    ('e83d433e-f36b-1410-8adb-00ab2e36627d', 'a958c2d1-72d1-4eed-a921-a05b4adee1da', 5, 'CityCommuter', 'The carbon fiber frame makes this so much lighter than other e-bikes. Hills feel like flat ground now.', GETDATE()),

    -- 943d... (Water Filter)
    ('943d433e-f36b-1410-8adb-00ab2e36627d', 'c0e0258d-26c1-47de-98f4-60bb3fa4d812', 4, 'HealthNut', 'Water tastes much cleaner. The flow rate is a bit slow on the ultrafiltration mode, but that is expected.', GETDATE()),

    -- b03c... (Haptic Controller)
    ('b03c433e-f36b-1410-8adb-00ab2e36627d', 'c7a10fc9-1778-4b86-ae57-b970c78c6042', 5, 'CasualGamer', 'The haptic feedback is subtle but adds a whole new level of immersion. Battery life is fantastic.', GETDATE()),

    -- f03d... (HiFi Headphones)
    ('f03d433e-f36b-1410-8adb-00ab2e36627d', 'e212b4d7-3e10-458b-8b4f-076ae39b606f', 5, 'AudiophileMike', 'Wide soundstage and very analytical. If you want to hear every breath in a recording, these are for you.', GETDATE()),

    -- cc3d... (Pro Laptop)
    ('cc3d433e-f36b-1410-8adb-00ab2e36627d', 'e50fe81d-ac13-4a6f-b137-cebc343fe575', 4, 'DevOps_Dan', 'The vapor chamber cooling actually works. Compiled a massive kernel and the fans barely ramped up.', GETDATE()),

    -- b83d... (Culinary Tech)
    ('b83d433e-f36b-1410-8adb-00ab2e36627d', 'e7e1c15e-11a8-41ac-a778-266f0596cbb8', 5, 'ChefInTraining', 'Perfect consistency for every batch. The IoT features are surprisingly helpful for long cooks.', GETDATE()),

    -- c03d... (Gaming Headset)
    ('c03d433e-f36b-1410-8adb-00ab2e36627d', 'f0f9814c-540d-43a1-b055-c4550dbd1325', 4, 'FragMaster', 'Surround sound is great for pinpointing footsteps. The mic is clear but a bit sensitive to background noise.', GETDATE()),

    -- dc3d... (Production PC)
    ('dc3d433e-f36b-1410-8adb-00ab2e36627d', 'f6ea95c2-c1a5-4a66-b660-99243cc3ba13', 5, 'StudioHead', 'Render times cut in half. This workstation is an absolute beast for 8K video production.', GETDATE()),

    -- d03d... (Satellite Comms)
    ('d03d433e-f36b-1410-8adb-00ab2e36627d', 'f930ce5a-c4c9-4ce0-8070-ddce82b69123', 5, 'SoloHiker', 'Peace of mind in the backcountry. Sent a check-in message from a deep canyon with no issues.', GETDATE());
