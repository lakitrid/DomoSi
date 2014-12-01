DomoSi
======

Briques de base pour un système domotique sur mesure

Le système à une architecture cible un peu atypique :
- Système de supervision général sur un serveur Windows avec une base de données fichier.
- Site Web de contrôle / consultation sur ce même serveur Windows
- Système de collecte des données sur un BeagleBone Black sous debian : système d'échange utilisant un service REST Mono,
Contrôle matèriel soit en C ou en utilisant des librairies Python
- Récupération des données physique soit directement sur le BeagleBone soit en passant par des Arduino déportés communiquant soit sur port série, soit en utilisant des NRF24L01
