# Sandbox Game Foundation for Unity

日本語の注意事項は下の方に書いてあります。


***caution***  
This project is a trial version before the alpha version.


### Preparation

- Unity (Supported 5.6.1+)
- Your favorite player character prefab or fbx.
- grounds texture.


### Usage:

1. Download this repository. (and extract)

2. Copy your favorite player character prefab or fbx to "Assets/Resources/Character/player/" directory.

3. Copy grounds texture to "Assets/Resources/Terrain/Grounds/" directory.

4. Edit player_fbx_filepath in "Congigs/player.json".

5. Edit texture_filepath in "Configs/terrain.json".

6. Open directory in unity and Build&Run.

if you not find Configs directory or each under files, it will be created when you open the directory and Build&Run with Unity.  

### Control player

It moves front, left, back, right with WASD.  
Can't do anything else.

- - -

***注意事項***  
このプロジェクトはα版以前の試作版です。


### 用意するもの

- Unity (バージョン5.6.1以降)
- お気に入りのプレイヤーキャラのprefabまたはfbxファイル
- Terrainに貼る地面テクスチャ


### 使い方

1. このリポジトリをダウンロードしてください。（zipを落とした場合は解凍も）

2. 用意したプレイヤーキャラのファイルを "Assets/Resources/Character/player/" ディレクトリにコピーしてください。

3. 用意した地面のテクスチャを "Assets/Resources/Terrain/Grounds/" ディレクトリにコピーしてください。

4. "Congigs/player.json" ファイルを開き、player_fbx_filepath を編集してください。

5. "Configs/terrain.json" ファイルを開き、texture_filepath を編集してください。

6. Unityでディレクトリを開き、Build & Run で動きます。

もしConfigsディレクトリやその中のファイルが見つからない場合、一旦UnityでBuild & Runすると生成されます。


### 操作方法

WASDで前後左右に動きます。  
それ以外のことは出来ません。
